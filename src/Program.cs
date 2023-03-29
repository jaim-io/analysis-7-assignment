using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Demos;
using TheCardGame.Effects;
using TheCardGame.Games;
using TheCardGame.Players;

var player1 = new Player("player1", 10);
var player2 = new Player("player2", 10);
var gb = GameBoard.GetInstance();
{
    var colourFactory = new DemoColourFactory();
    var colours = new Dictionary<string, Colour>
    {
        { "red",  colourFactory.CreateColour("Red") },
        { "blue",  colourFactory.CreateColour("Blue") },
        { "red-blue",  colourFactory.CreateDualColour("Red", "Blue") },
        { "colourless",  colourFactory.CreateColour("Colourless") },
    };

    var effectFactory = new DemoEffectFactory();
    var cardFactory = new DemoCardFactory();

    var p1_cs = cardFactory.CreateSpellCard("COUNTER-EFFECT", colours["red"]);
    var counterEffect = effectFactory.CreateCounterEffect("p1-ce", string.Empty);
    p1_cs.BindOnRevealEffect(counterEffect);

    var p1_hd = cardFactory.CreateSpellCard("HIDDEN-DANGER", colours["red"]);
    var sohEffect = effectFactory.CreateSleightOfHandEffect("soh1", string.Empty);
    p1_hd.BindOnRevealEffect(sohEffect);

    player1.SetCards(
        cards: new() {
            p1_cs,
            p1_hd,
            cardFactory.CreateLandCard("p1-red-land-1", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-2", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-3", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-4", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-5", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-6", colours["red"]),
            cardFactory.CreateSpellCard("p1-red-buff-1", colours["red"]), // Buffs creature for +5/+3
            cardFactory.CreateCreatureCard("p1-red-creature-1", colours["red"], 2, 2),
        });

    var p2_cs = cardFactory.CreateSpellCard("COUNTER-EFFECT", colours["red"]);
    var p2_cs_effect = effectFactory.CreateCounterEffect("p2-cs", string.Empty);
    p2_cs.BindOnRevealEffect(p2_cs_effect);

    player2.SetCards(
        cards: new() {
            p2_cs,
            cardFactory.CreateSpellCard("TEST", colours["red"]),
            cardFactory.CreateLandCard("p2-red-land-1", colours["red"]),
            cardFactory.CreateLandCard("p2-red-land-2", colours["red"]),
            cardFactory.CreateLandCard("p2-blue-land-1", colours["blue"]),
            cardFactory.CreateLandCard("p2-blue-land-2", colours["blue"]),
            cardFactory.CreateLandCard("p2-blue-land-3", colours["blue"]),
            cardFactory.CreateCreatureCard("p2-red-creature-1", colours["red"], 2, 2),
        });

    gb.SetPlayers(player1, player2, player1);
}

gb.SetupADemoSituation();
gb.LogCurrentSituation();

// ### The demo game ### //
if (Turns.Turn1A()) { goto End; }
if (Turns.Turn1B()) { goto End; }

End:
Console.WriteLine("==== End of demo");