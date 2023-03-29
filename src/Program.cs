using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Demos;
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
    var counterEffect = effectFactory.CreateCounterEffect("cf1", string.Empty);

    var cardFactory = new DemoCardFactory();
    player1.SetCards(
        cards: new() {
            cardFactory.CreateLandCard("p1-red-land-1", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-2", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-3", colours["red"]),
            cardFactory.CreateSpellCard("COUNTER-EFFECT", colours["red"], counterEffect),
            cardFactory.CreateLandCard("p1-red-land-4", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-5", colours["red"]),
            cardFactory.CreateLandCard("p1-red-land-6", colours["red"]),
            cardFactory.CreateSpellCard("p1-hidden-danger-1", colours["red"]),
            cardFactory.CreateSpellCard("p1-red-buff-1", colours["red"]), // Buffs creature for +5/+3
            cardFactory.CreateCreatureCard("p1-red-creature-1", colours["red"], 2, 2),
        });

    player2.SetCards(
        cards: new() {
            cardFactory.CreateSpellCard("TEST", colours["red"], effectFactory.CreateCounterEffect("cf1", string.Empty)),
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

// Turn 1A            
if (!gb.NewTurn()) { goto End; }
gb.PlayCard(player1.Id, "p1-red-land-1");

// Some arbitrary spell/effect
gb.PlayCard(player2.Id, "TEST");
gb.ActivateEffect(player2.Id, "TEST"); // => adds the effect to the stack

gb.PlayCard(player1.Id, "COUNTER-EFFECT");
gb.ActivateEffect(player1.Id, "COUNTER-EFFECT");

gb.Stack.Resolve(); // resolves the stack LIFO

gb.EndTurn();
gb.LogCurrentSituation();

// Turn 1B
gb.PrepareNewTurn();
if (!gb.NewTurn()) { goto End; }
// gb.DrawCard("land-3");
gb.EndTurn();
gb.LogCurrentSituation();

End:
Console.WriteLine("==== End of demo");