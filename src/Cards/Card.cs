using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class Card
{
    // private Action preRevealEffect
    // private Action onRevealEffect
    // private Action onDisposeEffect
    public Effect? Effect { get; init; }
    private int _energyCost = 0; // The amount of energy required to play this card.   
    private string _cardId; /* The unique id of this card in the game. */
    private List<ICardObserver> _observers = new();
    public IReadOnlyList<ICardObserver> Observers => _observers;
    public string Description { get; init; }
    public CardState State { get; set; }
    public Colour Colour { get; init; }

    public Card(
        string cardId,
        Colour colour,
        Effect? effect = null)
    {
        this._cardId = cardId;
        this.Colour = colour;
        this.Description = string.Empty;
        this.State = new InTheDeck(this);
        Effect = effect;
    }

    public string GetId()
    {
        return this._cardId;
    }

    public int GetEnergyCost()
    {
        return this._energyCost;
    }

    public void SetEnergyCost(int energyCost)
    {
        this._energyCost = energyCost;
    }

    public virtual int GetDefenseValue() { throw new NotImplementedException(); }
    public virtual int SubtractDefenseValue(int iAttackValue) { throw new NotImplementedException(); }
    public virtual int GetInitialAttackValue() { throw new NotImplementedException(); }
    public virtual int GetActualAttackValue() { throw new NotImplementedException(); }
    public virtual int GetEnergyLevel() { throw new NotImplementedException(); }

    public virtual bool Dispose()
    {
        return this.State.Dispose();
    }

    public virtual void OnEndTurn()
    {
        this.State.OnEndTurn();
    }

    public bool OnDraw()
    {
        return this.State.OnDraw();
    }

    public bool OnPlay()
    {
        return this.State.OnPlay();
    }

    public bool IsNotYetInTheGame()
    {
        return this.State.IsNotYetInTheGame();
    }

    public virtual void GoDefending() { }

    public virtual void GoAttacking() { }

    public virtual void PeformAttack() { }

    public virtual void TapEnergy() { }

    public virtual int GivesEnergyLevel()
    {
        return this.State.GivesEnergyLevel();
    }

    public void AddObserver(ICardObserver observer) => this._observers.Add(observer);
    public void RemoveObserver(ICardObserver observer) => this._observers.Remove(observer);
    public void ActivateEffect()
    {
        if (this.Effect is not null)
        {
            this.Effect.Activate();
        }
    }
}