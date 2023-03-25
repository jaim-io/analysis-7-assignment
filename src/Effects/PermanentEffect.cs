namespace TheCardGame.Effects;

public class PermanentEffect : Effect
{
    public PermanentEffect(Action value) : base(value)
    {
    }

    public override void Activate()
    {
        throw new NotImplementedException();
    }
}