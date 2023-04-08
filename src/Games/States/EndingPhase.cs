using TheCardGame.Games;

namespace TheCardGame.Games.States;

public class EndingPhase : GameState
{
    public EndingPhase(GameBoard game)
        : base(game)
    {
    }

    public override void ToPrepPhase()
    {
        this.game.State = new PreperationPhase(this.game);
    }
}