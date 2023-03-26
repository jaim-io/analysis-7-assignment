namespace TheCardGame.Effects;

public class PermanentEffect : Effect
{
    public PermanentEffect(
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