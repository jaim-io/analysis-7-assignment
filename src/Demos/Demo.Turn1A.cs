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
        
        gb.ToMainPhase();
        gb.PlayCard(player1, "p1-red-land-1");

        gb.ActivateEffect(player1, "HIDDEN-DANGER-CARD", "SleightOfHand");
        gb.Stack.Resolve(); 
        gb.PlayCard(player1, "HIDDEN-DANGER-CARD");

        gb.PlayCard(player2, "COUNTER-CARD");
        gb.ActivateEffect(player2, "COUNTER-CARD", "Counter"); 

        gb.PlayCard(player1, "COUNTER-CARD");
        gb.ActivateEffect(player1, "COUNTER-CARD", "Counter");

        gb.Stack.Resolve(); 

        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}