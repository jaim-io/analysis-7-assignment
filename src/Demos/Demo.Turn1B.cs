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
        gb.ActivateEffect(player1, "HIDDEN-DANGER-CARD", "DealDamageToAllCards");
        gb.ActivateEffect(player1, "HIDDEN-DANGER-CARD", "SkipDrawingPhase");

        if (!gb.ToDrawingPhase()) { return false; }
        gb.ToMainPhase();

        gb.Stack.Resolve();

        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}