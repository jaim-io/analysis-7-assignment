// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Effects.States;

public class OnTheStack : EffectState
{
    public OnTheStack(Effect effect)
        : base(effect)
    {
    }

    public override void Trigger()
    {
        this.effect.State = new Active(this);
        this.effect.Apply();
    }

    public override void Dispose()
    {
        this.effect.State = new Used(this);
    }
}