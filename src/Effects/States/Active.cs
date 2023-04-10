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
        this.effect.Owner!.RemoveObserver(this.effect);
        this.effect.State = new Used(this);
    }
}