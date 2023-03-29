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
}