using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Effects.States;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Effects.ConcreteEffects;

public abstract class SleightOfHandEffect : Effect
{
    private uint _currentTurn;
    protected SleightOfHandEffect(
        string name,
        string description)
        : base(new PreRevealEffect(), name, description, null, null)
    {
    }

    public override void Trigger()
    {
        this.State = new Active(this);
        this._currentTurn = GameBoard.GetInstance().Turn;
        GameBoard.GetInstance().AddObserver(this);
        this.Owner!.State = new OnTheBoardFaceDown(Owner.State);
        Console.WriteLine($"[Sleight of Hand] turned {this.Owner!.GetId()} face down.");
    }

    public override void StartOfTurn(StartOfTurnEvent eventInfo)
    {
        if (GameBoard.GetInstance().Turn >= this._currentTurn + 1)
        {
            this.State = new Used(this);
            GameBoard.GetInstance().RemoveObserver(this);
            this.Owner!.State = new OnTheBoardFaceUp(Owner.State);
            Console.WriteLine($"[Sleight of Hand] turned {this.Owner!.GetId()} face up.");
        }
    }
}