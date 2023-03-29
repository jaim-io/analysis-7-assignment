using TheCardGame.Games;
using TheCardGame.Players;

partial class Turns
{
    public static bool Turn1A()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        if (!gb.NewTurn()) { return false; }
        gb.PlayCard(player1, "p1-red-land-1");

        gb.PlayCard(player1, "HIDDEN-DANGER");
        gb.ActivateEffect(player1, "HIDDEN-DANGER"); // => Will activate PreRevealEffect
        gb.Stack.Resolve(); // Manual resolve to resolve the PreRevealEffect

        // Some arbitrary spell/effect
        gb.PlayCard(player2, "TEST");
        gb.ActivateEffect(player2, "TEST"); // => adds the effect to the stack

        gb.PlayCard(player1, "COUNTER-EFFECT");
        gb.ActivateEffect(player1, "COUNTER-EFFECT");

        gb.Stack.Resolve(); // resolves the stack LIFO

        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}