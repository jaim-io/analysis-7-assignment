namespace TheCardGame.Effects.States;

public abstract class EffectState
{
    public virtual void onIsTaken() { }
}

// IsActive
// HasBeenUsed
// HasNotBeenUsed

// Card.state(OnTheBoard).TriggerEffects
// this.card.TriggerEffects