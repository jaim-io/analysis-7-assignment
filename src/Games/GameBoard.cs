using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Players;
using TheCardGame.Players.Events;
using TheCardGame.Utils;

namespace TheCardGame.Games;

public class GameBoard : Entity, IPlayerObserver
{
    private static GameBoard? _instance;
    public Player CurrentTurnPlayer { get; private set; }
    public Player OpponentPlayer { get; private set; }
    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }
    public GameState State { get; set; }
    private uint _turn;
    private bool _gameEnded;
    public TheStack Stack { get; init; }

    private GameBoard()
    {
        this.Player1 = new Player("dummy1", 0);
        this.Player2 = new Player("dummy2", 0);
        this.CurrentTurnPlayer = this.Player1;
        this.OpponentPlayer = this.Player2;
        this.State = new PreperationPhase(this);
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
        this.Player1 = player1;
        this.Player2 = player2;
        if (this.Player1.GetName() == this.Player2.GetName())
        {
            throw new System.InvalidOperationException("The two players should have a unique name.");
        }
        this.CurrentTurnPlayer = currentTurnPlayer;
        if (currentTurnPlayer.GetName() == player1.GetName())
        {
            this.OpponentPlayer = this.Player2;
        }
        else
        {
            this.OpponentPlayer = this.Player1;
        }

        this.Player1.AddObserver(this);
        this.Player2.AddObserver(this);
    }

    public bool TakeCard()
    {
        return this.State.TakeCard();
    }

    public bool DrawCard(string cardId)
    {
        return this.State.DrawCard(cardId);
    }

    public bool NewTurn()
    {
        if (this._gameEnded)
        {
            return false;
        }
        this._turn++;
        return this.State.TakeCard();
    }

    public void EndTurn()
    {
        foreach (Card card in this.CurrentTurnPlayer.GetCards())
        {
            card.OnEndTurn();
        }
        foreach (Card card in this.OpponentPlayer.GetCards())
        {
            card.OnEndTurn();
        }
    }

    public void PrepareNewTurn()
    {
        this.CurrentTurnPlayer.TrimCards(5);
        this.SwapPlayer();
    }

    /* returns the current cards on the board for the specififed player */
    public List<Card> GetCardsOnBoard(Player player)
    {
        List<Card> cards = new List<Card>();

        if (player.GetName() == this.OpponentPlayer.GetName())
        {
            return Support.GetCardsCanBePlayed(this.OpponentPlayer.GetCards());
        }
        else
        {
            return Support.GetCardsCanBePlayed(this.CurrentTurnPlayer.GetCards());
        }
    }

    public Player GetCurrentTurnPlayer()
    {
        return this.CurrentTurnPlayer;
    }
    public Player GetOpponentPlayer()
    {
        return this.OpponentPlayer;
    }
    public uint GetCurrentTurn()
    {
        return this._turn;
    }

    public bool TurnCardFaceUp(Guid playerId, string cardId)
    {
        var player = GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        if (card == null || !Support.CardIsIn<OnTheBoardFaceDown>(card))
        {
            return false;
        }

        player.TurnCardFaceUp(card);
        return true;
    }

    public bool PlayCard(Guid playerId, string cardId)
    {
        var player = GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        if (card == null || !Support.CardIsIn<InTheHand>(card))
        {
            return false;
        }

        player.PlayCard(card);
        return true;
    }

    public void ActivateEffect(Guid playerId, string cardId, List<Entity>? targets = null)
    {
        var player = GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        card?.ActivateEffect(targets);
    }

    public bool PeformAttack(string cardId, List<string> opponentDefenseCardIds)
    {
        foreach (Card oCard in this.OpponentPlayer.GetCards())
        {
            foreach (string defenseCardId in opponentDefenseCardIds)
            {
                if (oCard.GetId() == defenseCardId)
                {
                    oCard.GoDefending();
                }
            }
        }

        (Card card, int iPos) = Support.FindCard(this.CurrentTurnPlayer.GetCards(), cardId);
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
        foreach (Card card in this.CurrentTurnPlayer.GetCards())
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
        foreach (Card card in this.CurrentTurnPlayer.GetCards())
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

    public void PlayerDied(PlayerDiedEvent pde)
    {
        Console.WriteLine($"Player {pde.PlayerName} died. Health: {pde.Health}, {pde.Reason}");
        if (pde.PlayerName == this.Player1.GetName())
        {
            Console.WriteLine($"Player {this.Player2.GetName()} is the winner!");
        }
        else
        {
            Console.WriteLine($"Player {this.Player1.GetName()} is the winner!");
        }
        this._gameEnded = true;
    }

    /* These are methods just for Demo stuff */
    public void SetupADemoSituation()
    {
        for (int cnt = 0; cnt < 6; cnt++)
        {
            this.Player1.DrawCard();
        }
        for (int cnt = 0; cnt < 6; cnt++)
        {
            this.Player2.DrawCard();
        }
    }

    public void LogCurrentSituation()
    {
        Console.WriteLine("\n==== Current situation");
        Console.WriteLine($"Current turn-player: {this.CurrentTurnPlayer.GetName()}, Turn: {this._turn}");
        Console.WriteLine($"Player {this.Player1.GetName()}: Health: {this.Player1.GetHealthValue()}");
        Console.WriteLine($"Player {this.Player2.GetName()}: Health: {this.Player2.GetHealthValue()}");

        List<Card> cards_player1 = this.Player1.GetCards();
        Console.WriteLine($"Player {this.Player1.GetName()}: (ontheboard/indeck/inhand/indiscard-pile) {Support.CountCards<OnTheBoardFaceUp>(cards_player1)}/{Support.CountCards<InTheDeck>(cards_player1)}/{Support.CountCards<InTheHand>(cards_player1)}/{Support.CountCards<OnTheDisposedPile>(cards_player1)}");
        Console.WriteLine($"Player {this.Player1.GetName()} on the board: " + Support.CardIdsHumanFormatted<OnTheBoardFaceUp>(cards_player1));
        Console.WriteLine($"Player {this.Player1.GetName()} in deck: " + Support.CardIdsHumanFormatted<InTheDeck>(cards_player1));
        Console.WriteLine($"Player {this.Player1.GetName()} in hand: " + Support.CardIdsHumanFormatted<InTheHand>(cards_player1));
        Console.WriteLine($"Player {this.Player1.GetName()} on the discard-pile: " + Support.CardIdsHumanFormatted<OnTheDisposedPile>(cards_player1));

        List<Card> cards_player2 = this.Player2.GetCards();
        Console.WriteLine($"Player {this.Player2.GetName()}: (ontheboard/indeck/inhand/indiscard-pile) {Support.CountCards<OnTheBoardFaceUp>(cards_player2)}/{Support.CountCards<InTheDeck>(cards_player2)}/{Support.CountCards<InTheHand>(cards_player2)}/{Support.CountCards<OnTheDisposedPile>(cards_player2)}");
        Console.WriteLine($"Player {this.Player2.GetName()} on the board: " + Support.CardIdsHumanFormatted<OnTheBoardFaceUp>(cards_player2));
        Console.WriteLine($"Player {this.Player2.GetName()} in deck: " + Support.CardIdsHumanFormatted<InTheDeck>(cards_player2));
        Console.WriteLine($"Player {this.Player2.GetName()} in hand: " + Support.CardIdsHumanFormatted<InTheHand>(cards_player2));
        Console.WriteLine($"Player {this.Player2.GetName()} on the discard-pile: " + Support.CardIdsHumanFormatted<OnTheDisposedPile>(cards_player2));

        Console.WriteLine("==== END Current situation\n");
    }

    private void SwapPlayer()
    {
        if (this.CurrentTurnPlayer.GetName() == this.Player1.GetName())
        {
            this.CurrentTurnPlayer = this.Player2;
            this.OpponentPlayer = this.Player1;
        }
        else
        {
            this.CurrentTurnPlayer = this.Player1;
            this.OpponentPlayer = this.Player2;
        }
    }

    private Player GetPlayerById(Guid playerId)
    {
        return playerId == this.Player1.Id
            ? Player1
            : playerId == this.Player2.Id
            ? Player2
            : throw new ArgumentException($"Invalid playerId. Value: {playerId}");
    }
}