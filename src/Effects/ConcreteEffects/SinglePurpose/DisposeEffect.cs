// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Effects.Types;

namespace TheCardGame.Effects.ConcreteEffects.SinglePurpose;

public class DisposeEffect : Effect
{
    public DisposeEffect()
        : base(new OnRevealEffect(), "Dispose", "Disposes the owner of this effect (card)", null)
    {
    }

    public override void Apply()
    {
        this.Owner!.Dispose();
        this.Dispose();
        Console.WriteLine($"[Dispose] {this.Owner!.GetId()} has been disposed.");
    }
}