using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Games.States;

namespace TheCardGame.Effects.ConcreteEffects.MultiPurpose;

public class DelayedDisposeEffect<T> : Effect
    where T : GameState
{
    private readonly Guid _playerId;
    private readonly Type _gameStateType;
    public DelayedDisposeEffect(Guid playerId)
        : base(new OnRevealEffect(), "DelayedDispose", "Disposes the owner of this effect (card) on the given phase <T>", null)
    {
        this._playerId = playerId;
        this._gameStateType = typeof(T) == typeof(GameState)
            ? throw new Exception("DelayedDisposeEffect<T> cannot be used with GameState as T.")
            : typeof(T);
    }

    public override void Apply()
    {
        GameBoard.GetInstance().AddObserver(this);
        Console.WriteLine($"[DelayedDispose] has been activated for {this.Owner!.GetId()}.");
    }

    public override void PreparationPhase(PreparationPhaseEvent eventInfo)
    {
        if (this._gameStateType == typeof(Games.States.PreperationPhase) && eventInfo.PlayerId == this._playerId)
        {
            this.DisposeOwner();
        }
    }
    public override void MainPhase(MainPhaseEvent eventInfo)
    {
        if (this._gameStateType == typeof(Games.States.MainPhase) && eventInfo.PlayerId == this._playerId)
        {
            this.DisposeOwner();
        }
    }
    public override void EndPhase(EndPhaseEvent eventInfo)
    {
        if (this._gameStateType == typeof(Games.States.EndingPhase) && eventInfo.PlayerId == this._playerId)
        {
            this.DisposeOwner();
        }
    }

    public void DisposeOwner()
    {
        GameBoard.GetInstance().RemoveObserver(this);
        this.Owner!.Dispose();
        this.Dispose();
        Console.WriteLine($"[DelayedDispose] {this.Owner!.GetId()} has been disposed.");
    }
}