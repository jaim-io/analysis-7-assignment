using TheCardGame.Cards.Colours;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Effects;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Cards;

public abstract class Card : Entity, IGameBoardObserver
{
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

    public virtual int GetAttackValue() { throw new NotImplementedException(); }
    public virtual void ResetAttackValue() { throw new NotImplementedException(); }
    public virtual void ModifyAttackValue(Func<int, int> modifier) { throw new NotImplementedException(); }

    public virtual int GetDefenseValue() { throw new NotImplementedException(); }
    public virtual void ResetDefenceValue() { throw new NotImplementedException(); }
    public virtual void ModifyDefenceValue(Func<int, int> modifier) { throw new NotImplementedException(); }

    public virtual int SubtractDefenseValue(int value) { throw new NotImplementedException(); }

    public virtual int GetEnergyLevel() { throw new NotImplementedException(); }

    public virtual bool Dispose()
    {
        return this.State.Dispose();
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
    public void RemoveAllObservers() => this._observers.RemoveAll(_ => true);


    public void ActivateEffect(string name, List<Entity>? targets = null) => this.State.ActivateEffect(name, targets);
    public void MainPhase(MainPhaseEvent eventInfo) { }
    public void PreparationPhase(PreparationPhaseEvent eventInfo) => this.State.OnStartTurn();
    public void EndPhase(EndPhaseEvent eventInfo) => this.State.OnEndTurn();

    public void TurnFaceUp() => this.State.TurnFaceUp();
}