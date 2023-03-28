namespace TheCardGame.Effects;

public abstract class PermanentEffect : Effect
{
    public PermanentEffect(
        string name,
        string description,
        Action action,
        Action revertAction,
        Func<bool> duration) 
        : base(name, description, action, revertAction, duration)
    {
    }
}