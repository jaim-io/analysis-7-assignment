namespace TheCardGame.Effects;

public class TemporaryEffect : Effect
{
    public TemporaryEffect(
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
// Observer needed?