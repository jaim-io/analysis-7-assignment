using TheCardGame.Cards.Colours;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer2(Dictionary<string, Func<Colour>> getColour)
    {
        var player = new Player("player2", 10);

        var effectFactory = new DemoEffectFactory();
        var cardFactory = new DemoCardFactory();

        var p2_cs = cardFactory.CreateSpellCard("COUNTER-CARD", getColour["red"]());
        var p2_cs_effect = effectFactory.CreateCounterEffect("COUNTER-EFFECT", string.Empty);
        p2_cs.BindEffect(p2_cs_effect);

        player.SetCards(
            cards: new() {
                p2_cs,
                cardFactory.CreateSpellCard("TEST", getColour["red"]()),
                cardFactory.CreateLandCard("p2-red-land-1", getColour["red"]()),
                cardFactory.CreateLandCard("p2-red-land-2", getColour["red"]()),
                cardFactory.CreateLandCard("p2-blue-land-1", getColour["blue"]()),
                cardFactory.CreateLandCard("p2-blue-land-2", getColour["blue"]()),
                cardFactory.CreateLandCard("p2-blue-land-3", getColour["blue"]()),
                cardFactory.CreateCreatureCard("p2-red-creature-1", getColour["red"](), 2, 2),
            });

        return player;
    }
}