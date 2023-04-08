using TheCardGame.Common.Models;
using TheCardGame.Effects;
using TheCardGame.Effects.ConcreteEffects.MultiPurpose;
using TheCardGame.Effects.ConcreteEffects.SinglePurpose;

namespace TheCardGame.Demos;

public class DemoEffectFactory : EffectFactory
{
    public override BuffCreatureEffect CreateBuffCreatureEffect(
        string name,
        string description,
        int attackOffset,
        int defenseOffset,
        uint amountOfTurns)
    {
        return new DemoBuffCreatureEffect(
            name,
            description,
            attackOffset,
            defenseOffset,
            amountOfTurns);
    }

    public override CounterEffect CreateCounterEffect()
    {
        return new DemoCounterEffect();
    }

    public override DealDamageEffect CreateDealDamageEffect(
        string name,
        string description,
        uint damage,
        Func<List<Entity>>? getPreDeterminedTargets = null)
    {
        return new DemoDealDamageEffect(
            name,
            description,
            damage,
            getPreDeterminedTargets);
    }

    public override DelayedDisposeEffect<T> CreateDelayedDisposeEffect<T>(Guid playerId)
    {
        return new DemoDelayedDisposeEffect<T>(playerId);
    }

    public override DiscardRandomCardEffect CreateDiscardRandomCardEffect()
    {
        return new DemoDiscardRandomCardEffect();
    }

    public override DisposeEffect CreateDisposeEffect()
    {
        return new DemoDisposeEffect();
    }

    public override ModifyAttackDamageEffect CreateModifyAttackDamageEffect(string name, Func<int, int> attackModifier, List<Type> creatureStates)
    {
        return new ModifyAttackDamageEffect(name, attackModifier, creatureStates);
    }

    public override SkipDrawingPhaseEffect CreateSkipDrawingPhaseEffect(uint amountOfTurns)
    {
        return new DemoSkipDrawingPhaseEffect(amountOfTurns);
    }

    public override SleightOfHandEffect<T> CreateSleightOfHandEffect<T>(uint amountOfTurns)
    {
        return new DemoSleightOfHandEffect<T>(amountOfTurns);
    }
}