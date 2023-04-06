using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Games;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static void SetupInitialScenario()
    {
        var gb = GameBoard.GetInstance();

        // Arnold
        {
            var redLand1 = gb.Player1.DrawCard("red-land-1");
            var redLand2 = gb.Player1.DrawCard("red-land-2");
            var redLand3 = gb.Player1.DrawCard("red-land-3");
            var redLand4 = gb.Player1.DrawCard("red-land-4");

            gb.Player1.PlayCard(redLand1!);
            gb.Player1.PlayCard(redLand2!);
            gb.Player1.PlayCard(redLand3!);
            gb.Player1.PlayCard(redLand4!);

            redLand1!.TapEnergy();
            redLand2!.TapEnergy();
        }


        // Bryce
        {
            gb.Player2.Cards.AddRange(new List<Card>()
                {
                    CardFactory.CreateLandCard("red-land-1", new() { ColourFactory.CreateRed() }),
                    CardFactory.CreateLandCard("red-land-2", new() { ColourFactory.CreateRed() }),
                    CardFactory.CreateLandCard("blue-land-1", new() { ColourFactory.CreateBlue() }),
                    CardFactory.CreateLandCard("blue-land-2", new() { ColourFactory.CreateBlue() }),
                    CardFactory.CreateLandCard("blue-land-3", new() { ColourFactory.CreateBlue() }),
                    CardFactory.CreateCreatureCard("red-creature-1", new() { ColourFactory.CreateRed() }, 2, 2),
                });

            var redLand1 = gb.Player2.DrawCard("red-land-1");
            var redLand2 = gb.Player2.DrawCard("red-land-2");
            var blueLand1 = gb.Player2.DrawCard("blue-land-1");
            var blueLand2 = gb.Player2.DrawCard("blue-land-2");
            var blueLand3 = gb.Player2.DrawCard("blue-land-3");
            var redCreature1 = gb.Player2.DrawCard("red-creature-1");

            gb.Player2.PlayCard(redLand1!);
            gb.Player2.PlayCard(redLand2!);
            gb.Player2.PlayCard(blueLand1!);
            gb.Player2.PlayCard(blueLand2!);
            gb.Player2.PlayCard(blueLand3!);
            gb.Player2.PlayCard(redCreature1!);
        }

        gb.DrawInitialCards();
    }
}