using TheCardGame.Games;

public class PreperationPhase : GameState
{
    public PreperationPhase(GameBoard game)
        : base(game)
    {       
    }

    public override void ToDrawingPhase()
    {
        this.game.State = new DrawingPhase(this.game);
    }
}