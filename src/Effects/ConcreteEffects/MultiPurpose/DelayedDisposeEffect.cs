using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Games.States;

namespace TheCardGame.Effects.ConcreteEffects.MultiPurpose;

public class DelayedDisposeEffect<T> : Effect
    where T : GameState
{
    private readonly Guid _playerId;
    public DelayedDisposeEffect(Guid playerId)
        : base(new OnRevealEffect(), "DelayedDispose", "Disposes the owner of this effect (card) on the given phase <T>", null, null)
    {
        this._playerId = playerId;
    }

    public override void Trigger()
    {
        GameBoard.GetInstance().AddObserver(this);
    }

    public override void PreparationPhase(PreparationPhaseEvent eventInfo)
    {
        if (typeof(T) == typeof(Games.States.PreperationPhase) && eventInfo.PlayerId == this._playerId)
        {
            this.DisposeOwner();
        }
    }
    public override void MainPhase(MainPhaseEvent eventInfo)
    {
        if (typeof(T) == typeof(Games.States.MainPhase) && eventInfo.PlayerId == this._playerId)
        {
            this.DisposeOwner();
        }
    }
    public override void EndPhase(EndPhaseEvent eventInfo)
    {
        if (typeof(T) == typeof(Games.States.EndingPhase) && eventInfo.PlayerId == this._playerId)
        {
            this.DisposeOwner();
        }
    }

    public void DisposeOwner()
    {
        GameBoard.GetInstance().RemoveObserver(this);
        this.Owner!.Dispose();
        this.Dispose();
    }
}