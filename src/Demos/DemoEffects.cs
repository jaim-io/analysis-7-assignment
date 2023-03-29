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