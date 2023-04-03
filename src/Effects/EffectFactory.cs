using TheCardGame.Common.Models;
using TheCardGame.Effects.ConcreteEffects;

namespace TheCardGame.Effects;

public abstract class EffectFactory
{
    public abstract CounterEffect CreateCounterEffect(string name, string description, Func<bool>? condition = null);
    public abstract SleightOfHandEffect CreateSleightOfHandEffect(string name, string description);
    public abstract DealDamageEffect CreateDealDamageEffect(string name, string description, uint damage, Func<List<Entity>>? getPreDeterminedTargets, Func<bool>? duration);
}