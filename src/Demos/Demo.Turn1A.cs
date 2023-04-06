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

        gb.PlayCard(player1, "blue-land-1");
        gb.PlayCard(player1, "blue-land-2");

        if (gb.PlayCard(player1, "hidden-danger")) // Sleight of Hand is activated automatically as it's an pre-reveal effect
        {
            // Arnold is able to play the card and is prompted to turn the land with the given colours
            Console.WriteLine("Arnold was able to play Hidden Danger");
            gb.TapFromCard("red-land-1");
            gb.TapFromCard("red-land-2");
            gb.TapFromCard("red-land-3");
            gb.TapFromCard("red-land-4");
            gb.TapFromCard("blue-land-1");
            gb.TapFromCard("blue-land-2");
        }

        gb.EndTurn();
        Console.WriteLine("=== Turn 1A [END]");
        gb.LogCurrentSituation();

        return true;
    }
}

// gb.PlayCard(player1, "p1-red-creature-1");
// gb.PlayCard(player1, "creature-buff-spell");
// {
//     var (creatureCard, _) = Support.FindCard(gb.Player1.GetCards(), "p1-red-creature-1");
//     gb.ActivateEffect(player1, "creature-buff-spell", "BuffCreatureOneTurn", new() { creatureCard });
//     gb.Stack.Resolve();
// }