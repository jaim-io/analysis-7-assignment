using TheCardGame.Games;

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