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
        gb.PlayCard(player1, "p1-red-land-1");
        gb.PlayCard(player1, "p1-red-land-2");
        gb.PlayCard(player1, "p1-red-land-3");

        gb.ActivateEffect(player1, "HIDDEN-DANGER-CARD", "SleightOfHand");
        gb.Stack.Resolve();
        gb.PlayCard(player1, "HIDDEN-DANGER-CARD");

        gb.PlayCard(player2, "COUNTER-CARD");
        gb.ActivateEffect(player2, "COUNTER-CARD", "Counter");

        gb.PlayCard(player1, "COUNTER-CARD");
        gb.ActivateEffect(player1, "COUNTER-CARD", "Counter");

        gb.Stack.Resolve();

        gb.PlayCard(player1, "p1-red-creature-1");
        gb.PlayCard(player1, "BUFF-CREATURE-CARD");
        {
            var (creatureCard, _) = Support.FindCard(gb.Player1.GetCards(), "p1-red-creature-1");
            gb.ActivateEffect(player1, "BUFF-CREATURE-CARD", "BuffCreatureOneTurn", new() { creatureCard });
            gb.Stack.Resolve();
        }

        gb.EndTurn();
        Console.WriteLine("=== Turn 1A [END]");
        gb.LogCurrentSituation();

        return true;
    }

    public static bool Temp()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        /* Setup starting situtation:
            - Arnold: 
                10 lives
                7 cards in hand
                4 red lands -> 2 land are in used state
            - Bryce: 
                10 lives
                7 cards in hand
                3 blue lands 
                2 red lands
                red creature 2-2
        */


        if (!gb.StartTurn()) { return false; } // => restore the 2 lands of Arnold and draw card
        // Arnold should have 8 cards

        gb.ToMainPhase();
        // Plays 2 lands (colour ??)
        // Get 6 energy of which 4 red
        // Play hidden danger
        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}