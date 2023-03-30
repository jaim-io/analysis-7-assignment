using TheCardGame.Games;

public class EndingPhase : GameState
{
    public EndingPhase(GameBoard game)
        : base(game)
    {
    }

    public override void NextState()
    {
        this.game.State = new PreperationPhase(this.game);
    }
}