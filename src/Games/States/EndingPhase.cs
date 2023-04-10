// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Games.States;

public class EndingPhase : GameState
{
    public EndingPhase(GameBoard game)
        : base(game)
    {
        Console.WriteLine($"[GameState] changed to EndingPhase");
    }

    public override void ToPrepPhase()
    {
        this.game.State = new PreperationPhase(this.game);
    }
}