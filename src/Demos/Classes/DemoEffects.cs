using TheCardGame.Common.Models;
using TheCardGame.Effects.ConcreteEffects.MultiPurpose;
using TheCardGame.Effects.ConcreteEffects.SinglePurpose;

namespace TheCardGame.Demos;

public class DemoCounterEffect : CounterEffect
{
    public DemoCounterEffect()
    {
    }
}

public class DemoSleightOfHandEffect : SleightOfHandEffect
{
    public DemoSleightOfHandEffect(uint amountOfTurns)
        : base(amountOfTurns)
    {
    }
}

public class DemoDealDamageEffect : DealDamageEffect
{
    public DemoDealDamageEffect(
        string name,
        string description,
        uint damage,
        Func<List<Entity>>? getPreDeterminedTargets = null)
        : base(name, description, damage, getPreDeterminedTargets)
    {
    }
}

public class DemoBuffCreatureEffect : BuffCreatureEffect
{
    public DemoBuffCreatureEffect(
        string name,
        string description,
        int attackOffset,
        int defenseOffset,
        uint amountOfTurns)
        : base(name, description, attackOffset, defenseOffset, amountOfTurns)
    {
    }
}

public class DemoSkipDrawingPhaseEffect : SkipDrawingPhaseEffect
{
    public DemoSkipDrawingPhaseEffect(uint amountOfTurns)
        : base(amountOfTurns)
    {
    }
}