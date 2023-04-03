using TheCardGame.Common.Models;
using TheCardGame.Effects;
using TheCardGame.Effects.ConcreteEffects;

namespace TheCardGame.Demos;

public class DemoEffectFactory : EffectFactory
{
    public override CounterEffect CreateCounterEffect(
        string name,
        string description,
        Func<bool>? condition = null)
    {
        return new DemoCounterEffect(name, description, condition);
    }

    public override DealDamageEffect CreateDealDamageEffect(
        string name,
        string description,
        uint damage,
        Func<List<Entity>>? getPreDeterminedTargets = null,
        Func<bool>? duration = null)
    {
        return new DemoDealDamageEffect(name, description, damage, getPreDeterminedTargets, duration);
    }

    public override SleightOfHandEffect CreateSleightOfHandEffect(
        string name,
        string description)
    {
        return new DemoSleightOfHandEffect(name, description);
    }
}