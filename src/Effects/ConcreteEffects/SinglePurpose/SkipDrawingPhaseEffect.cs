using TheCardGame.Effects.Types;
using TheCardGame.Games.Events;
using TheCardGame.Players;
using TheCardGame.Players.Constraints;

namespace TheCardGame.Effects.ConcreteEffects.SinglePurpose;

public class SkipDrawingPhaseEffect : Effect
{
    public SkipDrawingPhaseEffect()
        : base(
            new OnRevealEffect(),
            "SkipDrawingPhase",
            "This effect will skip the drawing phase of the opponent",
            null,
            null)
    {
    }

    public override void Trigger()
    {
        this._userInvokedTargets.ForEach(entity =>
        {
            if (entity is Player player)
            {
                player.Constraints.Add(new SkipDrawing());
            }
        });
    }

    public override void EndOfTurn(EndOfTurnEvent eventInfo)
    {
        this._userInvokedTargets.ForEach(entity =>
        {
            if (entity is Player player)
            {
                player.Constraints.Remove(new SkipDrawing());
            }
        });
        this.Dispose();
    }
}