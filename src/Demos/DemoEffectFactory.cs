using TheCardGame.Effects;

namespace TheCardGame.Demos;

public class DemoEffectFactory
{
    public Effect CreateEffect(
        string name,
        string description,
        Action action,
        Action? revertAction = null,
        Func<bool>? condition = null) => new DemoEffect(
                                    name,
                                    description,
                                    action,
                                    revertAction,
                                    condition);
}