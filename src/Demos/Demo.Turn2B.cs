using TheCardGame.Games;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static bool Turn2B()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        gb.PrepareNewTurn();

        Console.WriteLine("=== Turn 2B [Start]");
        if (!gb.StartTurn()) { return false; }

        if (!gb.ToDrawingPhase()) { return false; }

        gb.ToMainPhase();


        if (gb.PlayCard(player2, "artefact-1"))
        {
            gb.TapFromCard("red-land-1");
            gb.TapFromCard("blue-land-1");

            gb.ActivateEffect(player2, "artefact-1", "DelayedDispose"); 
            gb.ActivateEffect(player2, "artefact-1", "SkipDrawPhase");
            gb.ActivateEffect(player2, "artefact-1", "AllCreaturesDealHalfDamage");
            gb.Stack.Resolve();
        };
        
        /*
            if (playcard(player1, "damage-spell")){
                tapFromCard("red-land-3");
                tapFromCard("red-land-4");

                activateEffect(player2, artefact, "Dispose");
                activateEffect(player1, "damage-spell", "DealDamage", new() { redCreature1 });
                resolveStack();
            }
        */

        gb.EndTurn();
        Console.WriteLine("=== Turn 2B [End]");
        gb.LogCurrentSituation();

        return true;
    }
}