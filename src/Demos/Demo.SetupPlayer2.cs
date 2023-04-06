using TheCardGame.Cards.Colours;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer2()
    {
        var player = new Player("Bryce", 10);

        var counterCard = CardFactory.CreateSpellCard("counter-spell", new() { ColourFactory.CreateRed() });
        {
            var counterEffect = EffectFactory.CreateCounterEffect();
            counterCard.BindEffect(counterEffect);
        }

        player.SetCards(
            cards: new() {
                counterCard,
                CardFactory.CreateSpellCard("TEST", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p2-red-land-1", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p2-red-land-2", new() { ColourFactory.CreateRed() }),
                CardFactory.CreateLandCard("p2-blue-land-1", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateLandCard("p2-blue-land-2", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateLandCard("p2-blue-land-3", new() { ColourFactory.CreateBlue() }),
                CardFactory.CreateCreatureCard("p2-red-creature-1", new() { ColourFactory.CreateRed() }, 2, 2),
            });

        return player;
    }
}