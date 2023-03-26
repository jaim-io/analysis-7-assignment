using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Players.Events;
using TheCardGame.Utils;

namespace TheCardGame.Players;

public class Player
{
    private List<Card> _cards;
    private int _healthValue;
    private string _name = string.Empty;
    public Guid Id { get; init; }

    private List<PlayerObserver> _observers = new List<PlayerObserver>();

    public Player(string name, int initialLife)
    {
        this.Id = Guid.NewGuid();
        this._cards = new List<Card>();
        this._healthValue = initialLife;
        this._name = name;
    }

    public void AddObserver(PlayerObserver po)
    {
        this._observers.Add(po);
    }

    public void RemoveObserver(PlayerObserver po)
    {
        this._observers.Remove(po);
    }

    public void SetCards(List<Card> cards)
    {
        this._cards = cards;
    }

    public List<Card> GetCards()
    {
        return this._cards;
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
            foreach (PlayerObserver po in this._observers)
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
        foreach (Card card in this._cards)
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
        foreach (PlayerObserver po in this._observers)
        {
            po.PlayerDied(pde);
        }
        return null;
    }

    /* Draw a card from his hand */
    public Card? DrawCard(string cardId)
    {
        foreach (Card card in this._cards)
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

    public void TrimCards(int maxCards)
    {
        int cnt = Support.CountCards<InTheHand>(this._cards);
        if (cnt <= maxCards)
        {
            Console.WriteLine($"{this.GetName()} trimmed 0 cards into discard pile.");
            return;
        }

        int cntDisposed = 0;
        foreach (Card card in this._cards)
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

            cnt = Support.CountCards<InTheHand>(this._cards);
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