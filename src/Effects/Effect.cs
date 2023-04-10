using TheCardGame.Cards;
using TheCardGame.Cards.Events;
using TheCardGame.Common.Models;
using TheCardGame.Effects.States;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Players;
using TheCardGame.Players.Events;

namespace TheCardGame.Effects;

public abstract class Effect : ICardObserver, IGameBoardObserver
{
    protected List<Entity> _userInvokedTargets = new();
    protected Func<List<Entity>> _getPreDeterminedTargets;
    public Card? Owner { get; set; } = null;
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public EffectType Type { get; init; }
    public EffectState State { get; set; }

    public Effect(
        EffectType type,
        string name,
        string description,
        Func<List<Entity>>? getPreDeterminedTargets = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        Name = name;
        Description = description;
        State = new Unused(this);
        this._getPreDeterminedTargets = getPreDeterminedTargets ?? (() => new List<Entity>());
    }

    public void Trigger() => this.State.Trigger();
    public abstract void Apply();

    public void Activate(List<Entity>? targets = null)
    {
        this._userInvokedTargets = targets ?? new();
        this.State.Activate();
    }

    public void ActivateWithoutStack()
    {
        this.State.ActivateWithoutStack();
    }

    public void Dispose() => this.State.Dispose();

    public virtual void CardDisposed(CardDisposedEvent eventInfo) { }
    public virtual void PreparationPhase(PreparationPhaseEvent eventInfo) { }
    public virtual void MainPhase(MainPhaseEvent eventInfo) { }
    public virtual void EndPhase(EndPhaseEvent eventInfo) { }
}