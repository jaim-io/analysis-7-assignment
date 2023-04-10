// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Common.Models;
using TheCardGame.Effects.ConcreteEffects.MultiPurpose;
using TheCardGame.Effects.ConcreteEffects.SinglePurpose;
using TheCardGame.Games.States;

namespace TheCardGame.Effects;

public abstract class EffectFactory
{
    public abstract CounterEffect CreateCounterEffect();
    public abstract SleightOfHandEffect<T> CreateSleightOfHandEffect<T>(uint amountOfTurns)
        where T : GameState;
    public abstract DealDamageEffect CreateDealDamageEffect(string name, string description, uint damage, Func<List<Entity>>? getPreDeterminedTargets);
    public abstract BuffCreatureEffect CreateBuffCreatureEffect(string name, string description, int attackOffset, int defenseOffset, uint amountOfTurns);
    public abstract SkipDrawingPhaseEffect CreateSkipDrawingPhaseEffect(uint amountOfTurns);
    public abstract DisposeEffect CreateDisposeEffect();
    public abstract DiscardRandomCardEffect CreateDiscardRandomCardEffect();
    public abstract ModifyAttackDamageEffect CreateModifyAttackDamageEffect(string name, Func<int, int> attackModifier, List<Type> creatureStates);
    public abstract DelayedDisposeEffect<T> CreateDelayedDisposeEffect<T>(Guid playerId)
        where T : GameState;
}