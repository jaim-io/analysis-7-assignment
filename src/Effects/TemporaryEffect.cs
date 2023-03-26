namespace TheCardGame.Effects;

public class TemporaryEffect : Effect
{
    public TemporaryEffect(
        string name,
        string description,
        Action action)
        : base(name, description, action)
    {
    }

    public override void Activate()
    {
        throw new NotImplementedException();
    }
}
// Observer needed?