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
        if (!gb.NewTurn()) { return false; }

        gb.ToMainPhase();

        gb.ActivateEffect(player1, "HIDDEN-DANGER-CARD", "DealDamageToAllCards");
        gb.Stack.Resolve();

        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}