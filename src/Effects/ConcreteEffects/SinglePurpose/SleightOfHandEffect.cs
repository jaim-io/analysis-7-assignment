using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Effects.States;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Games.States;

namespace TheCardGame.Effects.ConcreteEffects.SinglePurpose;

public abstract class SleightOfHandEffect<T> : Effect
    where T : GameState
{
    private uint _startingTurn;
    private uint _amountOfTurns;
    private Type _gameStateType;
    protected SleightOfHandEffect(
        uint amountOfTurns)
        : base(
            new PreRevealEffect(),
            "SleightOfHand",
            $"This effect will place the owner of the effect (the card) face down on the board, and will turn the owner face up after {amountOfTurns} {(amountOfTurns > 1 ? "Turns" : "Turn")}",
            null,
            null)
    {
        this._amountOfTurns = amountOfTurns;
        this._gameStateType =
            typeof(T) == typeof(GameState)
            ? throw new Exception("SleightOfHandEffect<T> cannot be used with GameState as T.")
            : typeof(T);
    }

    public override void Apply()
    {
        this._startingTurn = GameBoard.GetInstance().Turn;
        GameBoard.GetInstance().AddObserver(this);
        this.Owner!.State = new OnTheBoardFaceDown(Owner.State);
        Console.WriteLine($"[Sleight of Hand] turned {this.Owner!.GetId()} face down.");
    }

    public override void PreparationPhase(PreparationPhaseEvent eventInfo)
    {
        if (this._gameStateType == typeof(PreperationPhase))
        {
            this.TurnCardFaceUp();
        }
    }

    public override void MainPhase(MainPhaseEvent eventInfo)
    {
        if (this._gameStateType == typeof(MainPhase))
        {
            this.TurnCardFaceUp();
        }
    }

    private void TurnCardFaceUp()
    {
        if (GameBoard.GetInstance().Turn >= this._startingTurn + this._amountOfTurns)
        {
            this.Owner!.TurnFaceUp();
            GameBoard.GetInstance().RemoveObserver(this);
            this.Dispose();
            Console.WriteLine($"[Sleight of Hand] turned {this.Owner!.GetId()} face up.");
        }
    }
}