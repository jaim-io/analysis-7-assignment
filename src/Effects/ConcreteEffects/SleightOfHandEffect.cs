using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Effects.States;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Effects.ConcreteEffects;

public abstract class SleightOfHandEffect : Effect
{
    protected SleightOfHandEffect(
        string name,
        string description,
        Func<bool>? duration = null)
        : base(new PreRevealEffect(), name, description, null, duration)
    {
    }

    public override void Trigger()
    {
        this.State = new Active(this);
        GameBoard.GetInstance().AddObserver(this);
        this.Owner!.State = new OnTheBoardFaceDown(Owner.State);
    }

    public override void StartOfTurn(StartOfTurnEvent eventInfo)
    {
        GameBoard.GetInstance().RemoveObserver(this);
        this.State = new Used(this);
    }
}