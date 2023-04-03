using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Effects;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Cards;

public abstract class Card : Entity, IGameBoardObserver
{
    private int _energyCost = 0; // The amount of energy required to play this card.   
    private string _cardId; /* The unique id of this card in the game. */
    private List<ICardObserver> _observers = new();
    public IReadOnlyList<ICardObserver> Observers => _observers;
    public List<Effect> Effects { get; init; } = new();
    public string Description { get; init; }
    public CardState State { get; set; }
    public List<Colour> Colours { get; init; }

    public Card(
        string cardId,
        List<Colour> colour,
        List<Effect>? effects = null)
    {
        this._cardId = cardId;
        Colours = colour.ToList();
        this.Description = string.Empty;
        this.State = new InTheDeck(this);
        this.Effects = effects ?? this.Effects;
    }

    public Card BindEffect(Effect effect)
    {
        if (effect.Owner is not null)
        {
            throw new Exception($"Effect can only be bound once. Given Effect with ID: {effect.Id}");
        }

        effect.Owner = this;
        this.Effects.Add(effect);
        this.AddObserver(effect);

        return this;
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

    public virtual int GetInitialDefenseValue() { throw new NotImplementedException(); }
    public virtual int GetInitialAttackValue() { throw new NotImplementedException(); }
    public virtual int GetAttackValue() { throw new NotImplementedException(); }
    public virtual int SubtractDefenseValue(int value) { throw new NotImplementedException(); }

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
    public void ActivateEffect(string name, List<Entity>? targets = null) => this.State.ActivateEffect(name, targets);

    public void StartOfTurn(StartOfTurnEvent eventInfo) => this.State.OnStartTurn();
    public void EndOfTurn(EndOfTurnEvent eventInfo) => this.State.OnEndTurn();
}