using TheCardGame.Cards.Colours;
using TheCardGame.Demos;
using TheCardGame.Games;

var gb = GameBoard.GetInstance();
{
    var player1 = Demo.SetupPlayer1();
    var player2 = Demo.SetupPlayer2();
    gb.SetPlayers(player1, player2, player1);
}

Console.WriteLine("==== Start of demo\n");
Demo.SetupInitialScenario();
if (!Demo.Turn1A()) { goto End; }
if (!Demo.Turn1B()) { goto End; }
if (!Demo.Turn2A()) { goto End; }
if (!Demo.Turn2B()) { goto End; }

End:
Console.WriteLine("==== End of demo");