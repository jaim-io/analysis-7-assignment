using TheCardGame.Games;

public class PreperationPhase : GameState
{
    public PreperationPhase(GameBoard game)
        : base(game)
    {       
    }

    public override void NextState()
    {
        this.game.State = new DrawingPhase(this.game);
    }
}