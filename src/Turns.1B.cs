using TheCardGame.Games;
using TheCardGame.Players;

partial class Turns
{
    public static bool Turn1B()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        gb.PrepareNewTurn();
        if (!gb.NewTurn()) { return false; }

        gb.TurnCardFaceUp(player1, "HIDDEN-DANGER");
        gb.ActivateEffect(player1, "HIDDEN-DANGER"); // => Will activate ONRevealEffect
        gb.Stack.Resolve(); // Manual resolve to resolve the OnRevealEffect

        gb.EndTurn();
        gb.LogCurrentSituation();

        return true;
    }
}