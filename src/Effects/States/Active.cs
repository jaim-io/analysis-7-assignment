namespace TheCardGame.Effects.States;

public class Active : EffectState
{
    public Active(Effect effect)
        : base(effect)
    {
    }

    public Active(EffectState state)
        : base(state.effect)
    {
    }

    public override void Dispose()
    {
        if (this.effect.Duration is not null)
        {
            this.effect.Revert();
        }
        this.effect.State = new Used(this);
    }
}