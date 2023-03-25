namespace TheCardGame.Effects.States;

public abstract class EffectState
{
    public Effect effect { get; set; }
    public EffectState(Effect effect)
    {
        this.effect = effect;
    }
    public virtual void OnIsTaken() { }
    public virtual void Activate() { }
}

// Card.state(OnTheBoard).TriggerEffects
// this.card.TriggerEffects