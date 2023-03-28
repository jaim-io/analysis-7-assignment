using TheCardGame.Cards;
using TheCardGame.Cards.Events;
using TheCardGame.Effects.States;
using TheCardGame.Players;
using TheCardGame.Players.Events;

namespace TheCardGame.Effects;

public abstract class Effect : IPlayerObserver, ICardObserver
{
    protected Action _action;
    protected Action? _revertAction;
    // Condition null example => deal 4 damage to opponent
    public Func<bool>? Condition { get; init; }
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public EffectState State { get; set; }

    public Effect(
        string name,
        string description,
        Action action,
        Action? revertAction = null,
        Func<bool>? condition = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        State = new Unused(this);
        _action = action;
        _revertAction = revertAction;
        Condition = condition;
    }

    public virtual void Activate() => this.State.Activate();
    public void Revert()
    {
        if (this._revertAction is not null)
        {
            this._revertAction();
        }
    }
    public void Trigger() => this._action();
    public void Dispose() => this.State.Dispose();

    public void PlayerDied(PlayerDiedEvent eventInfo)
    {
        // Game over, no action needed.
    }

    public void CardDisposed(CardDisposedEvent eventInfo)
    {
        if (Condition != null)
        {
            if (Condition() is false)
            {
                this.Dispose();
            }
        }
    }
}

/*
Fields: 
    Duration? 

Methods: 
    Dispose?
    OnEndTurn
*/ 