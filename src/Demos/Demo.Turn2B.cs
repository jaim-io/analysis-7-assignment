using TheCardGame.Games;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static bool Turn2B()
    {
        var gb = GameBoard.GetInstance();
        var player1 = gb.Player1.Id;
        var player2 = gb.Player2.Id;

        return true;
    }
}