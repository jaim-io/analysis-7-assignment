using TheCardGame.Common.Models;
using TheCardGame.Effects.ConcreteEffects.MultiPurpose;
using TheCardGame.Effects.ConcreteEffects.SinglePurpose;

namespace TheCardGame.Effects;

public abstract class EffectFactory
{
    public abstract CounterEffect CreateCounterEffect();
    public abstract SleightOfHandEffect CreateSleightOfHandEffect(uint amountOfTurns);
    public abstract DealDamageEffect CreateDealDamageEffect(string name, string description, uint damage, Func<List<Entity>>? getPreDeterminedTargets);
    public abstract BuffCreatureEffect CreateBuffCreatureEffect(string name, string description, int attackOffset, int defenseOffset, uint amountOfTurns);
    public abstract SkipDrawingPhaseEffect CreateSkipDrawingPhaseEffect(uint amountOfTurns);
}