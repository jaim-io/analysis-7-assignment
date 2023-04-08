using TheCardGame.Effects.Types;
using TheCardGame.Games;

namespace TheCardGame.Effects.ConcreteEffects.SinglePurpose;

public abstract class CounterEffect : Effect
{
    public CounterEffect()
        : base(
            new OnRevealEffect(), 
            "Counter", 
            "Counters an effect the opponent triggered.", 
            null, 
            null)
    {
    }

    public override void Apply()
    {
        GameBoard.GetInstance().Stack.Skip(1);
        Console.WriteLine($"[Counter] a spell has been countered.");
        this.Dispose();
    }
}