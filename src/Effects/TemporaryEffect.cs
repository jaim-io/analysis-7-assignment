namespace TheCardGame.Effects;

public class TemporaryEffect : Effect
{
    public TemporaryEffect(Action value) : base(value)
    {
    }

    public override void Activate()
    {
        throw new NotImplementedException();
    }
}
// Observer needed?