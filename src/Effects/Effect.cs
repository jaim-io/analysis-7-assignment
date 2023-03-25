using TheCardGame.Effects.States;

namespace TheCardGame.Effects;

public abstract class Effect
{
    public Guid Id { get; init; }
    public string Description { get; private set; }
    public Action Action { get; private set; }
    public EffectState State { get; private set; }

    public Effect(
        Guid id,
        Action action,
        string description)
    {
        Id = id;
        Description = description;
        Action = action;
        State = new Unused(this);
    }

    public virtual void Activate() => this.State.Activate();
}

/*
Fields: 
    Duration? 

Methods: 
    Dispose?
    OnEndTurn
*/ 