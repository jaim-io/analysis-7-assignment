using TheCardGame.Cards;
using TheCardGame.Cards.Events;
using TheCardGame.Common;
using TheCardGame.Effects.States;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Players;
using TheCardGame.Players.Events;

namespace TheCardGame.Effects;

public abstract class Effect : Entity, IPlayerObserver, ICardObserver, IGameBoardObserver
{
    protected List<Entity> _targets = new();
    public Func<bool>? Duration { get; init; }
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public EffectState State { get; set; }

    public Effect(
        string name,
        string description,
        Func<bool>? duration = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        State = new Unused(this);
        Duration = duration;
    }

    public abstract void Trigger();

    public void Activate(List<Entity>? targets = null)
    {
        this._targets = targets ?? new();
        this.State.Activate();
    }
    public void Dispose() => this.State.Dispose();

    public virtual void Revert() { }
    public virtual void PlayerDied(PlayerDiedEvent eventInfo) { }
    public virtual void CardDisposed(CardDisposedEvent eventInfo) { }
    public virtual void StartOfTurn(StartOfTurnEvent eventInfo) { }
    public virtual void EndOfTurn(EndOfTurnEvent eventInfo) { }
}