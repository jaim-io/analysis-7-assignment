using TheCardGame.Cards;
using TheCardGame.Cards.Demos;
using TheCardGame.Games;
using TheCardGame.Players;

namespace TheCardGame;

public class Program
{
    public static void SetupPlayersAndCards()
    {
        // #TODO: remember to change the following script as it is NOT for the assignment.
        // IT is just an example.

        Player player1 = new Player("player1", 10);
        Player player2 = new Player("player2", 10);

        DemoGameFactory factory = new DemoGameFactory();

        List<Card> player1_cards = new List<Card>();
        player1_cards.Add(factory.CreateSpellCard("sorcery-1"));
        player1_cards.Add(factory.CreateSpellCard("sorcery-2"));
        player1_cards.Add(factory.CreateSpellCard("sorcery-3"));
        player1_cards.Add(factory.CreateLandCard("land-1"));
        player1_cards.Add(factory.CreateLandCard("land-2"));
        player1_cards.Add(factory.CreateCreatureCard("creature-1"));

        List<Card> player2_cards = new List<Card>();
        player2_cards.Add(factory.CreateSpellCard("sorcery-4"));
        player2_cards.Add(factory.CreateSpellCard("sorcery-5"));
        player2_cards.Add(factory.CreateSpellCard("sorcery-6"));
        player2_cards.Add(factory.CreateLandCard("land-3"));
        player2_cards.Add(factory.CreateLandCard("land-4"));
        player2_cards.Add(factory.CreateCreatureCard("creature-3"));

        player1.SetCards(player1_cards);
        player2.SetCards(player2_cards);

        GameBoard gb = GameBoard.GetInstance();
        gb.SetPlayers(player1, player2, player1);
    }

    public static void SetupACurrentSituation()
    {
        GameBoard gb = GameBoard.GetInstance();
        gb.SetupACurrentSituation();
    }

    public static void RunADemoGame()
    {
        GameBoard gb = GameBoard.GetInstance();

        //Player 1 - Turn 1                
        if (!gb.NewTurn()) { return; }
        gb.DrawCard("land-1");
        gb.EndTurn();
        gb.LogCurrentSituation();

        //Player 2  - Turn 2
        gb.PrepareNewTurn();
        if (!gb.NewTurn()) { return; }
        gb.DrawCard("land-3");
        gb.EndTurn();
        gb.LogCurrentSituation();
    }

    public static void Main(string[] args)
    {
        SetupPlayersAndCards();
        SetupACurrentSituation();
        GameBoard gb = GameBoard.GetInstance();
        gb.LogCurrentSituation();

        RunADemoGame();
    }
}