using TheCardGame.Effects;

namespace TheCardGame.Demos;

public class DemoEffect : Effect
{
    public DemoEffect(
        string name,
        string description,
        Action action,
        Action? revertAction = null,
        Func<bool>? condition = null)
        : base(name, description, action, revertAction, condition)
    {
    }
}