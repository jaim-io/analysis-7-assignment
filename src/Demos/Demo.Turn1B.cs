using TheCardGame.Games;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static bool Turn1B()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        gb.PrepareNewTurn();

        if (!gb.StartTurn()) { return false; }
        gb.ActivateEffect(player1, "hidden-danger", "DealDamageToAllCards");
        gb.ActivateEffect(player1, "hidden-danger", "SkipDrawingPhase", new() { gb.Player2 });
        gb.Stack.Resolve();

        if (!gb.ToDrawingPhase()) { return false; }
        gb.ToMainPhase();


        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}