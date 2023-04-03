using TheCardGame.Common.Models;
using TheCardGame.Effects;
using TheCardGame.Effects.ConcreteEffects;

namespace TheCardGame.Demos;

public class DemoCounterEffect : CounterEffect
{
    public DemoCounterEffect(
        string name,
        string description,
        Func<bool>? condition = null)
        : base(name, description, condition)
    {
    }
}

public class DemoSleightOfHandEffect : SleightOfHandEffect
{
    public DemoSleightOfHandEffect(
        string name,
        string description)
        : base(name, description)
    {
    }
}

public class DemoDealDamageEffect : DealDamageEffect
{
    public DemoDealDamageEffect(
        string name,
        string description,
        uint damage,
        Func<List<Entity>>? getPreDeterminedTargets = null, 
        Func<bool>? condition = null)
        : base(name, description, damage, getPreDeterminedTargets, condition)
    {
    }
}