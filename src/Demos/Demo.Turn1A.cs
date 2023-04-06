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

        gb.TapFromCard("red-land-1");
        gb.TapFromCard("red-land-2");
        gb.TapFromCard("red-land-3");
        gb.TapFromCard("red-land-4");
        gb.TapFromCard("blue-land-1");
        gb.TapFromCard("blue-land-2");

        gb.ActivateEffect(player1, "hidden-danger", "SleightOfHand");
        gb.Stack.Resolve();
        gb.PlayCard(player1, "hidden-danger");

        gb.EndTurn();
        Console.WriteLine("=== Turn 1A [END]");
        gb.LogCurrentSituation();

        return true;
    }
}



// var gb = GameBoard.GetInstance();
// var player1 = gb.Player1.Id;
// var player2 = gb.Player2.Id;

// Console.WriteLine("=== Turn 1A [Start]");
// if (!gb.StartTurn()) { return false; }


// if (!gb.ToDrawingPhase()) { return false; }

// gb.ToMainPhase();

// gb.ActivateEffect(player1, "hidden-danger", "SleightOfHand");
// gb.Stack.Resolve();
// gb.PlayCard(player1, "hidden-danger");

// gb.PlayCard(player2, "counter-spell");
// gb.ActivateEffect(player2, "counter-spell", "Counter");

// gb.PlayCard(player1, "counter-spell");
// gb.ActivateEffect(player1, "counter-spell", "Counter");
// gb.TapFromCard("red-land-1");

// gb.Stack.Resolve();

// gb.PlayCard(player1, "p1-red-creature-1");
// gb.PlayCard(player1, "creature-buff-spell");
// {
//     var (creatureCard, _) = Support.FindCard(gb.Player1.GetCards(), "p1-red-creature-1");
//     gb.ActivateEffect(player1, "creature-buff-spell", "BuffCreatureOneTurn", new() { creatureCard });
//     gb.Stack.Resolve();
// }

// gb.EndTurn();
// Console.WriteLine("=== Turn 1A [END]");
// gb.LogCurrentSituation();

// return true;