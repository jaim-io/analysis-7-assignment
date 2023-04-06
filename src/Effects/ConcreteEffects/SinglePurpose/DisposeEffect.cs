using TheCardGame.Effects.Types;

namespace TheCardGame.Effects.ConcreteEffects.SinglePurpose;

public class DisposeEffect : Effect
{
    public DisposeEffect()
        : base(new OnRevealEffect(), "Dispose", "Disposes the owner of this effect (card)", null, null)
    {
    }

    public override void Trigger()
    {
        this.Owner!.Dispose();
        this.Dispose();
    }
}