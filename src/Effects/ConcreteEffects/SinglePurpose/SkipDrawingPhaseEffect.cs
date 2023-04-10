// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Players;
using TheCardGame.Players.Constraints;

namespace TheCardGame.Effects.ConcreteEffects.SinglePurpose;

public class SkipDrawingPhaseEffect : Effect
{
    private uint _startingTurn;
    private uint _amountOfTurns;
    public SkipDrawingPhaseEffect(uint amountOfTurns)
        : base(
            new OnRevealEffect(),
            "SkipDrawingPhase",
            $"This effect will skip the drawing phase of the opponent for {amountOfTurns} {(amountOfTurns > 1 ? "Turns" : "Turn")}",
            null)
    {
        this._amountOfTurns = amountOfTurns - 1;
    }

    
    public override void Apply()
    {
        this._startingTurn = GameBoard.GetInstance().Turn;

        GameBoard.GetInstance().AddObserver(this);

        this._userInvokedTargets.ForEach(entity =>
        {
            if (entity is Player player)
            {
                player.Constraints.Add(new SkipDrawing());
            }
        });
    }

    public override void EndPhase(EndPhaseEvent eventInfo)
    {
        if (GameBoard.GetInstance().Turn >= this._startingTurn + this._amountOfTurns)
        {
            this._userInvokedTargets.ForEach(entity =>
            {
                if (entity is Player player)
                {
                    player.Constraints.RemoveWhere(c => c is SkipDrawing);
                }
            });
            this.Dispose();
            GameBoard.GetInstance().RemoveObserver(this);
        }
    }
}