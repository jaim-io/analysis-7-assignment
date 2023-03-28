namespace TheCardGame.Effects;

public abstract class TemporaryEffect : Effect
{
    /* 
        _duration contains a condition that is checked every turn
        if the condition is true, the effect is still active
        if the condition is false, the effect is reverted
    */
    public TemporaryEffect(
        string name,
        string description,
        Action action,
        Action revertAction,
        Func<bool> duration)
        : base(name, description, action, revertAction, duration)
    {
    }
}