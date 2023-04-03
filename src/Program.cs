using TheCardGame.Cards.Colours;
using TheCardGame.Demos;
using TheCardGame.Games;

var gb = GameBoard.GetInstance();
{
    var colourFactory = new DemoColourFactory();
    var colours = new Dictionary<string, Func<Colour>>
    {
        { "red",  () => colourFactory.CreateColour("Red") },
        { "blue",  () => colourFactory.CreateColour("Blue") },
        { "red-blue", () => colourFactory.CreateColour(new List<string> { "Red", "Blue" }) },
        { "colourless",  () => colourFactory.CreateColour("Colourless") },
    };

    var player1 = Demo.SetupPlayer1(colours);
    var player2 = Demo.SetupPlayer2(colours);

    gb.SetPlayers(player1, player2, player1);
}

Console.WriteLine("==== Start of demo");
if (!Demo.Turn1A()) { goto End; }
if (!Demo.Turn1B()) { goto End; }

End:
Console.WriteLine("==== End of demo");