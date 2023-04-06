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
        gb.ActivateEffect(player1, "hidden-danger", "Dispose");
        gb.ActivateEffect(player1, "hidden-danger", "SkipDrawingPhase", new() { gb.Player2 });
        gb.ActivateEffect(player1, "hidden-danger", "DealDamageToAllCards");
        gb.Stack.Resolve(); // Stack is resolved Last In First Out (LIFO)

        if (!gb.ToDrawingPhase()) { return false; }
        gb.ToMainPhase();

        gb.PlayCard(player2, "red-land-3");

        if (gb.PlayCard(player2, "known-game")) // Sleight of Hand is activated automatically as it's an pre-reveal effect
        {
            // Bryce is able to play the card and is prompted to turn the land with the given colours   
            Console.WriteLine("Bryce was able to play Known Game");
            gb.TapFromCard(player2, "blue-land-1");
            gb.TapFromCard(player2, "red-land-1");
            gb.TapFromCard(player2, "red-land-2");
            gb.TapFromCard(player2, "red-land-3");
        }

        gb.EndTurn();
        Console.WriteLine("=== Turn 1B [End]");
        gb.LogCurrentSituation();

        return true;
    }
}