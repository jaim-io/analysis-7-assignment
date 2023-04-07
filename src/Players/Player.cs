using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Players.Constraints;
using TheCardGame.Players.Events;
using TheCardGame.Utils;

namespace TheCardGame.Players;

public class Player : Entity
{
    public List<Card> Cards { get; private set; }
    private int _healthValue;
    private string _name = string.Empty;
    public Guid Id { get; init; }
    public HashSet<Constraint> Constraints { get; private set; }

    private List<IPlayerObserver> _observers = new List<IPlayerObserver>();

    public Player(string name, int initialLife)
    {
        this.Id = Guid.NewGuid();
        this.Cards = new List<Card>();
        this._healthValue = initialLife;
        this._name = name;
        this.Constraints = new();
    }

    public void AddObserver(IPlayerObserver po)
    {
        this._observers.Add(po);
    }

    public void RemoveObserver(IPlayerObserver po)
    {
        this._observers.Remove(po);
    }

    public void SetCards(List<Card> cards)
    {
        this.Cards = cards;
    }

    public List<Card> GetCards()
    {
        return this.Cards;
    }

    public string GetName()
    {
        return this._name;
    }

    public void DecreaseHealthValue(int iValue)
    {
        this._healthValue -= iValue;
        if (this._healthValue <= 0)
        {
            PlayerDiedEvent pde = new PlayerDiedEvent(this.GetName(), this.GetHealthValue(), "Health below or is zero");
            foreach (IPlayerObserver po in this._observers)
            {
                po.PlayerDied(pde);
            }
        }
    }
    public int GetHealthValue()
    {
        return this._healthValue;
    }

    /* Take the first card from his deck and put it in his hand */
    public Card? DrawCard()
    {
        foreach (Card card in this.Cards)
        {
            if (card.IsNotYetInTheGame())
            {
                if (card.OnDraw() is true)
                {
                    return card;
                }
            }
        }

        PlayerDiedEvent pde = new PlayerDiedEvent(this.GetName(), this.GetHealthValue(), "No more cards in deck");
        foreach (IPlayerObserver po in this._observers)
        {
            po.PlayerDied(pde);
        }
        return null;
    }

    /* Draw a card from his hand */
    public Card? DrawCard(string cardId)
    {
        foreach (Card card in this.Cards)
        {
            if (card.GetId() == cardId)
            {
                if (card.OnDraw() is true)
                {
                    return card;
                }
            }
        }
        return null;
    }

    public Card? DiscardRandomCard()
    {
        if (this.Cards.Count == 0)
        {
            return null;
        }

        Card card;
        do
        {
            var index = new Random().Next(0, this.Cards.Count);
            card = this.Cards[index];
        } while (card.State is not InTheHand);

        this.Cards.Remove(card);
        return card;
    }

    public void TrimCards(int maxCards)
    {
        int cnt = Support.CountCards<InTheHand>(this.Cards);
        if (cnt <= maxCards)
        {
            Console.WriteLine($"[{this.GetName()}] trimmed 0 cards into discard pile.\n");
            return;
        }

        int cntDisposed = 0;
        foreach (Card card in this.Cards)
        {
            if (Support.CardIsIn<InTheHand>(card))
            {
                bool isDisposed = card.Dispose();
                if (isDisposed)
                {
                    Console.WriteLine($"Card {card.GetId()} is disposed.");
                    cntDisposed++;
                }
            }

            cnt = Support.CountCards<InTheHand>(this.Cards);
            if (cnt <= maxCards)
            {
                break;
            }
        }
        Console.WriteLine($"Disposed {cntDisposed} cards");
    }

    public bool PlayCard(Card card)
    {
        return card.OnPlay();
    }
}