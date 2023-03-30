using TheCardGame.Cards;
using TheCardGame.Cards.States;

namespace TheCardGame.Effects.ConcreteEffects;

public abstract class SleightOfHandEffect : Effect
{
    protected SleightOfHandEffect(
        string name,
        string description,
        Func<bool>? duration = null)
        : base(name, description, null, duration)
    {
    }

    public override void Trigger()
    {
        this.Owner!.State = new OnTheBoardFaceDown(Owner.State);
    }
}