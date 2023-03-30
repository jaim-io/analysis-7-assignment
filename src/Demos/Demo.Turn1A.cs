using TheCardGame.Games;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static bool Turn1A()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        if (!gb.NewTurn()) { return false; }
        gb.PlayCard(player1, "p1-red-land-1");

        gb.PlayCard(player1, "HIDDEN-DANGER-CARD");
        gb.ActivateEffect(player1, "HIDDEN-DANGER-CARD", "SLEIGHT-OF-HAND");
        gb.Stack.Resolve(); 

        gb.PlayCard(player2, "COUNTER-CARD");
        gb.ActivateEffect(player2, "COUNTER-CARD", "COUNTER-EFFECT"); 

        gb.PlayCard(player1, "COUNTER-CARD");
        gb.ActivateEffect(player1, "COUNTER-CARD", "COUNTER-EFFECT");

        gb.Stack.Resolve(); 

        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}