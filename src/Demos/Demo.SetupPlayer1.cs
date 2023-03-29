using TheCardGame.Cards.Colours;
using TheCardGame.Players;

namespace TheCardGame.Demos;

public partial class Demo
{
    public static Player SetupPlayer1(Dictionary<string, Func<Colour>> getColour)
    {
        var player = new Player("player1", 10);

        var effectFactory = new DemoEffectFactory();
        var cardFactory = new DemoCardFactory();

        var p1_cs = cardFactory.CreateSpellCard("COUNTER-EFFECT", getColour["red"]());
        var counterEffect = effectFactory.CreateCounterEffect("p1-ce", string.Empty);
        p1_cs.BindOnRevealEffect(counterEffect);

        var p1_hd = cardFactory.CreateSpellCard("HIDDEN-DANGER", getColour["red"]());
        var sohEffect = effectFactory.CreateSleightOfHandEffect("soh1", string.Empty);
        p1_hd.BindOnRevealEffect(sohEffect);

        player.SetCards(
            cards: new() {
            p1_cs,
            p1_hd,
            cardFactory.CreateLandCard("p1-red-land-1", getColour["red"]()),
            cardFactory.CreateLandCard("p1-red-land-2", getColour["red"]()),
            cardFactory.CreateLandCard("p1-red-land-3", getColour["red"]()),
            cardFactory.CreateLandCard("p1-red-land-4", getColour["red"]()),
            cardFactory.CreateLandCard("p1-red-land-5", getColour["red"]()),
            cardFactory.CreateLandCard("p1-red-land-6", getColour["red"]()),
            cardFactory.CreateSpellCard("p1-red-buff-1", getColour["red"]()), // Buffs creature for +5/+3
            cardFactory.CreateCreatureCard("p1-red-creature-1", getColour["red"](), 2, 2),
            });

        return player;
    }
}