using TheCardGame.Cards.Colours;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer2()
    {
        var player = new Player("player2", 10);

        var p2_cs = CardFactory.CreateSpellCard("COUNTER-CARD", new() { ColourFactory.CreateRed() });
        var p2_cs_effect = EffectFactory.CreateCounterEffect("COUNTER-EFFECT", string.Empty);
        p2_cs.BindEffect(p2_cs_effect);

        player.SetCards(
            cards: new() {
                p2_cs,
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