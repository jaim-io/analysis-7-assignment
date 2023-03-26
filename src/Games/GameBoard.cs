using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Players;
using TheCardGame.Players.Events;
using TheCardGame.Utils;

namespace TheCardGame.Games;

public class GameBoard : PlayerObserver
{
    private static GameBoard? _instance;
    private Player _player1;
    private Player _player2;
    private Player _currentTurnPlayer;
    private Player _opponentPlayer;
    private uint _turn;
    private bool _gameEnded;

    public TheStack Stack { get; init; }

    private GameBoard()
    {
        this._player1 = new Player("dummy1", 0);
        this._player2 = new Player("dummy2", 0);
        this._currentTurnPlayer = this._player1;
        this._opponentPlayer = this._player2;
        this._turn = 0;
        this._gameEnded = false;
        this.Stack = new();
    }

    public static GameBoard GetInstance()
    {
        if (_instance is not GameBoard)
        {
            _instance = new();
        }
        return _instance;
    }

    public static void StartNew() => _instance = new();

    public void SetPlayers(Player player1, Player player2, Player currentTurnPlayer)
    {
        this._player1 = player1;
        this._player2 = player2;
        if (this._player1.GetName() == this._player2.GetName())
        {
            throw new System.InvalidOperationException("The two players should have a unique name.");
        }
        this._currentTurnPlayer = currentTurnPlayer;
        if (currentTurnPlayer.GetName() == player1.GetName())
        {
            this._opponentPlayer = this._player2;
        }
        else
        {
            this._opponentPlayer = this._player1;
        }

        this._player1.AddObserver(this);
        this._player2.AddObserver(this);
    }

    public bool TakeCard()
    {
        Player currentTurnPlayer = this.GetCurrentTurnPlayer();
        Card? card = currentTurnPlayer.DrawCard();
        if (card == null)
        {
            Console.WriteLine($"{currentTurnPlayer.GetName()} could not take card.");
            return false;
        }
        else
        {
            Console.WriteLine($"{currentTurnPlayer.GetName()} took card {card.GetId()} from deck into hand.");
            return true;
        }
    }

    public bool DrawCard(string cardId)
    {
        Player currentTurnPlayer = this.GetCurrentTurnPlayer();
        Card? card = currentTurnPlayer.DrawCard(cardId);
        if (card is null)
        {
            Console.WriteLine($"{currentTurnPlayer.GetName()} Didn't draw card {cardId}: Not in his hand.");
            return false;
        }

        Console.WriteLine($"{currentTurnPlayer.GetName()} draw card {card.GetId()}.");
        return true;
    }

    public bool NewTurn()
    {
        if (this._gameEnded)
        {
            return false;
        }
        this._turn++;
        this.TakeCard();
        return true;
    }

    public void EndTurn()
    {
        foreach (Card card in this._currentTurnPlayer.GetCards())
        {
            card.OnEndTurn();
        }
        foreach (Card card in this._opponentPlayer.GetCards())
        {
            card.OnEndTurn();
        }
    }

    public void PrepareNewTurn()
    {
        this._currentTurnPlayer.TrimCards(5);
        this.SwapPlayer();
    }

    /* returns the current cards on the board for the specififed player */
    public List<Card> GetCardsOnBoard(Player player)
    {
        List<Card> cards = new List<Card>();

        if (player.GetName() == this._opponentPlayer.GetName())
        {
            return Support.GetCardsCanBePlayed(this._opponentPlayer.GetCards());
        }
        else
        {
            return Support.GetCardsCanBePlayed(this._currentTurnPlayer.GetCards());
        }
    }

    public Player GetCurrentTurnPlayer()
    {
        return this._currentTurnPlayer;
    }
    public Player GetOpponentPlayer()
    {
        return this._opponentPlayer;
    }
    public uint GetCurrentTurn()
    {
        return this._turn;
    }

    public bool PlayCard(string cardId)
    {
        (Card card, int _) = Support.FindCard(this._currentTurnPlayer.GetCards(), cardId);
        if (card == null || !Support.CardIsIn<InTheHand>(card))
        {
            return false;
        }

        this._currentTurnPlayer.PlayCard(card);
        return true;
    }

    // public bool ActivateEffect(string cardId)
    // {

    // }

    public bool PeformAttack(string cardId, List<string> opponentDefenseCardIds)
    {
        foreach (Card oCard in this._opponentPlayer.GetCards())
        {
            foreach (string defenseCardId in opponentDefenseCardIds)
            {
                if (oCard.GetId() == defenseCardId)
                {
                    oCard.GoDefending();
                }
            }
        }

        (Card card, int iPos) = Support.FindCard(this._currentTurnPlayer.GetCards(), cardId);
        CreatureCard? attackCard = card as CreatureCard;
        if (attackCard is not null)
        {
            attackCard.GoAttacking();
            if (this.EnergyTapped() >= attackCard.GetEnergyCost())
            {
                attackCard.PeformAttack();
                return true;
            }
        }
        return false;
    }

    /* Tap Energry from a land-card currently on the board 
    Returns the energy-level tapped.*/
    public void TapFromCard(string cardId)
    {
        foreach (Card card in this._currentTurnPlayer.GetCards())
        {
            if (card.GetId() == cardId)
            {
                card.TapEnergy();
            }
        }
    }

    public int EnergyTapped()
    {
        int iSumEnergy = 0;
        foreach (Card card in this._currentTurnPlayer.GetCards())
        {
            LandCard? landCard = card as LandCard;
            if (landCard is not null)
            {
                iSumEnergy += landCard.GivesEnergyLevel();
            }
        }
        Console.WriteLine($"Energy-tapped: {iSumEnergy}");
        return iSumEnergy;
    }

    public override void PlayerDied(PlayerDiedEvent pde)
    {
        Console.WriteLine($"Player {pde.PlayerName} died. Health: {pde.Health}, {pde.Reason}");
        if (pde.PlayerName == this._player1.GetName())
        {
            Console.WriteLine($"Player {this._player2.GetName()} is the winner!");
        }
        else
        {
            Console.WriteLine($"Player {this._player1.GetName()} is the winner!");
        }
        this._gameEnded = true;
    }

    /* These are methods just for Demo stuff */
    public void SetupADemoSituation()
    {
        for (int cnt = 0; cnt < 6; cnt++)
        {
            this._player1.DrawCard();
        }
        for (int cnt = 0; cnt < 6; cnt++)
        {
            this._player2.DrawCard();
        }
    }

    public void LogCurrentSituation()
    {
        Console.WriteLine("==== Current situation");
        Console.WriteLine($"Current turn-player: {this._currentTurnPlayer.GetName()}, Turn: {this._turn}");
        Console.WriteLine($"Player {this._player1.GetName()}: Health: {this._player1.GetHealthValue()}");
        Console.WriteLine($"Player {this._player2.GetName()}: Health: {this._player2.GetHealthValue()}");

        List<Card> cards_player1 = this._player1.GetCards();
        Console.WriteLine($"Player {this._player1.GetName()}: (ontheboard/indeck/inhand/indiscard-pile) {Support.CountCards<OnTheBoard>(cards_player1)}/{Support.CountCards<InTheDeck>(cards_player1)}/{Support.CountCards<InTheHand>(cards_player1)}/{Support.CountCards<OnTheDisposedPile>(cards_player1)}");
        Console.WriteLine($"Player {this._player1.GetName()} on the board: " + Support.CardIdsHumanFormatted<OnTheBoard>(cards_player1));
        Console.WriteLine($"Player {this._player1.GetName()} in deck: " + Support.CardIdsHumanFormatted<InTheDeck>(cards_player1));
        Console.WriteLine($"Player {this._player1.GetName()} in hand: " + Support.CardIdsHumanFormatted<InTheHand>(cards_player1));
        Console.WriteLine($"Player {this._player1.GetName()} on the discard-pile: " + Support.CardIdsHumanFormatted<OnTheDisposedPile>(cards_player1));

        List<Card> cards_player2 = this._player2.GetCards();
        Console.WriteLine($"Player {this._player2.GetName()}: (ontheboard/indeck/inhand/indiscard-pile) {Support.CountCards<OnTheBoard>(cards_player2)}/{Support.CountCards<InTheDeck>(cards_player2)}/{Support.CountCards<InTheHand>(cards_player2)}/{Support.CountCards<OnTheDisposedPile>(cards_player2)}");
        Console.WriteLine($"Player {this._player2.GetName()} on the board: " + Support.CardIdsHumanFormatted<OnTheBoard>(cards_player2));
        Console.WriteLine($"Player {this._player2.GetName()} in deck: " + Support.CardIdsHumanFormatted<InTheDeck>(cards_player2));
        Console.WriteLine($"Player {this._player2.GetName()} in hand: " + Support.CardIdsHumanFormatted<InTheHand>(cards_player2));
        Console.WriteLine($"Player {this._player2.GetName()} on the discard-pile: " + Support.CardIdsHumanFormatted<OnTheDisposedPile>(cards_player2));

        Console.WriteLine("==== END Current situation\n");
    }

    private void SwapPlayer()
    {
        if (this._currentTurnPlayer.GetName() == this._player1.GetName())
        {
            this._currentTurnPlayer = this._player2;
            this._opponentPlayer = this._player1;
        }
        else
        {
            this._currentTurnPlayer = this._player1;
            this._opponentPlayer = this._player2;
        }
    }
}