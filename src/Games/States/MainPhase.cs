using TheCardGame.Games;

public class MainPhase : GameState
{
    public MainPhase(GameBoard game)
        : base(game)
    {
    }

    public override void NextState()
    {
        this.game.State = new EndingPhase(this.game);
    }
}