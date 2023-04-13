// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Games;
using TheCardGame.Utils;

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

        if (gb.PlayCard(player2, "p2-artefact-1"))
        {
            // Bryce is able to play the card and is prompted to turn the lands with the specified colours   
            gb.TapFromCard("p2-red-land-1");
            gb.TapFromCard("p2-blue-land-1");

            gb.ActivateEffect(player2, "p2-artefact-1", "DelayedDispose");
            gb.ActivateEffect(player2, "p2-artefact-1", "SkipDrawingPhase", new() { gb.Player1 });
            gb.ActivateEffect(player2, "p2-artefact-1", "AllCreaturesDealHalfDamage");
            gb.Stack.Resolve();
        }

        if (gb.PlayCard(player2, "p2-red-damage-spell-1"))
        {
            // Bryce is able to play the card and is prompted to turn the land with the specified colour   
            gb.TapFromCard("p2-red-land-2");

            var (creature, _) = Support.FindCard(gb.Player1.GetCards(), "p1-red-creature-1");
            gb.ActivateEffect(player2, "p2-red-damage-spell-1", "Dispose");
            gb.ActivateEffect(player2, "p2-red-damage-spell-1", "DealDamage", new() { creature });
            gb.Stack.Resolve();
        }

        gb.EndTurn();
        Console.WriteLine("=== Turn 2B [End]");
        gb.LogCurrentSituation();

        return true;
    }
}