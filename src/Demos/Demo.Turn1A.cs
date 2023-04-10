using TheCardGame.Games;
using TheCardGame.Utils;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static bool Turn1A()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        Console.WriteLine("=== Turn 1A [Start]");
        if (!gb.StartTurn()) { return false; }

        if (!gb.ToDrawingPhase()) { return false; }

        gb.ToMainPhase();

        gb.PlayCard(player1, "p1-red-land-6");
        gb.PlayCard(player1, "p1-red-land-7");

        if (gb.PlayCard(player1, "p1-hidden-danger")) // Sleight of Hand is activated automatically as it's an pre-reveal effect
        {
            // Arnold is able to play the card and is prompted to turn the land with the specified colours
            gb.TapFromCard("p1-red-land-1");
            gb.TapFromCard("p1-red-land-2");
            gb.TapFromCard("p1-red-land-3");
            gb.TapFromCard("p1-red-land-4");
            gb.TapFromCard("p1-red-land-6");
            gb.TapFromCard("p1-red-land-7");
        }

        gb.EndTurn();
        Console.WriteLine("=== Turn 1A [END]");
        gb.LogCurrentSituation();

        return true;
    }
}