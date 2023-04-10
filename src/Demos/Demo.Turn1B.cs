// Jamey Schaap 0950044
// Vincent de Gans 1003196

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

        Console.WriteLine("=== Turn 1B [Start]");
        if (!gb.StartTurn()) { return false; }

        // |-> Hidden Danger is turned from face down to face up
        gb.ActivateEffect(player1, "p1-hidden-danger", "Dispose");
        gb.ActivateEffect(player1, "p1-hidden-danger", "SkipDrawingPhase", new() { gb.Player2 });
        gb.ActivateEffect(player1, "p1-hidden-danger", "DealDamageToAllCards");
        gb.Stack.Resolve(); // Stack is resolved Last In First Out (LIFO)

        if (!gb.ToDrawingPhase()) { return false; }
        gb.ToMainPhase();

        gb.PlayCard(player2, "p2-red-land-3");

        if (gb.PlayCard(player2, "p2-known-game")) // Sleight of Hand is activated automatically as it's an pre-reveal effect
        {
            // Bryce is able to play the card and is prompted to turn the land with the specified colours   
            gb.TapFromCard("p2-blue-land-1");
            gb.TapFromCard("p2-red-land-1");
            gb.TapFromCard("p2-red-land-2");
            gb.TapFromCard("p2-red-land-3");
        }

        gb.EndTurn();
        Console.WriteLine("=== Turn 1B [End]");
        gb.LogCurrentSituation();

        return true;
    }
}