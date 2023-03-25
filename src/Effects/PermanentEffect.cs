namespace TheCardGame.Effects;

public class PermanentEffect : Effect
{
    public PermanentEffect(
        Guid id,
        Action action,
        string description)
        : base(id, action, description)
    {
    }

    public override void Activate()
    {
        throw new NotImplementedException();
    }
}