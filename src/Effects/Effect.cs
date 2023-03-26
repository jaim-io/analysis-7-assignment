using TheCardGame.Effects.States;

namespace TheCardGame.Effects;

public abstract class Effect
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Action Action { get; init; }
    public EffectState State { get; private set; }

    public Effect(
        string name,
        string description,
        Action action)
    {
        Id = Guid.NewGuid();
        Name = name;
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