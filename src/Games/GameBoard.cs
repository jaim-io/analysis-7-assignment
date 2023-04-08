using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Games.Events;
using TheCardGame.Games.States;
using TheCardGame.Players;
using TheCardGame.Players.Events;
using TheCardGame.Utils;

namespace TheCardGame.Games;

public class GameBoard : Entity, IPlayerObserver
{
    private static GameBoard? _instance;
    public Player CurrentPlayer { get; private set; }
    public Player OpponentPlayer { get; private set; }
    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }
    public GameState State { get; set; }
    public uint Turn { get; private set; }
    private bool _gameEnded;
    public TheStack Stack { get; init; }
    public List<IGameBoardObserver> Observers { get; private set; } = new();

    private GameBoard()
    {
        this.Player1 = new Player("dummy1", 0);
        this.Player2 = new Player("dummy2", 0);
        this.CurrentPlayer = this.Player1;
        this.OpponentPlayer = this.Player2;
        this.State = new PreperationPhase(this);
        this.Turn = 0;
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
    public void AddObserver(IGameBoardObserver observer) => this.Observers.Add(observer);
    public void RemoveObserver(IGameBoardObserver observer) => this.Observers.Remove(observer);
    public void SetPlayers(Player player1, Player player2, Player currentTurnPlayer)
    {
        this.Player1 = player1;
        this.Player2 = player2;
        if (this.Player1.GetName() == this.Player2.GetName())
        {
            throw new System.InvalidOperationException("The two players should have a unique name.");
        }
        this.CurrentPlayer = currentTurnPlayer;
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

    public void ToMainPhase() => this.State.ToMainPhase();
    public bool StartTurn()
    {
        if (this._gameEnded)
        {
            return false;
        }

        this.Turn++;

        this.State = new PreperationPhase(this);

        var prepPhaseEvent = new PreparationPhaseEvent(Turn, this.CurrentPlayer.Id);

        // Deep clone _observers so observers are able to remove themself safely from the _observer list.
        Observers
            .ConvertAll(o => o)
            .ForEach(o =>
            {
                // LandCards are reset in the preparation phase of it's owner.
                if (o is LandCard card)
                {
                    this.CurrentPlayer.GetCards().ForEach(c =>
                    {
                        if (c.GetId() == card.GetId())
                        {
                            c.PreparationPhase(prepPhaseEvent);
                        }
                    });
                }
                else
                {
                    o.PreparationPhase(prepPhaseEvent);
                }
            });

        return true;
    }

    public bool ToDrawingPhase()
    {
        this.State.ToDrawingPhase();

        return this.State.TakeCard();
    }

    public void EndTurn()
    {
        this.State.ToEndPhase();
    }

    public void PrepareNewTurn()
    {
        this.CurrentPlayer.TrimCards(5);
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
            return Support.GetCardsCanBePlayed(this.CurrentPlayer.GetCards());
        }
    }

    public bool PlayCard(Guid playerId, string cardId)
    {
        return this.State.PlayCard(playerId, cardId);
    }

    public void ActivateEffect(Guid playerId, string cardId, string effectName, List<Entity>? targets = null)
    {
        this.State.ActivateEffect(playerId, cardId, effectName, targets);
    }

    public bool PeformAttack(string cardId, List<string>? opponentDefenseCardIds = null)
    {
        return this.State.PeformAttack(cardId, opponentDefenseCardIds ?? new());
    }

    public void SetCardToAttacking(string cardId) => this.State.SetCardToAttacking(cardId);

    /* Tap Energry from a land-card currently on the board 
    Returns the energy-level tapped.*/
    public void TapFromCard(string cardId)
    {
        this.State.TapFromCard(cardId);
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

    public void LogCurrentSituation()
    {
        Console.WriteLine("\n==== Current situation");

        Console.WriteLine($"Current turn-player: {this.CurrentPlayer.GetName()}, Turn: {this.Turn}");
        Console.WriteLine($"Player {this.Player1.GetName()}: Health: {this.Player1.GetHealthValue()}");
        Console.WriteLine($"Player {this.Player2.GetName()}: Health: {this.Player2.GetHealthValue()}");

        List<Card> cards_player1 = this.Player1.GetCards();
        Console.WriteLine($"Player {this.Player1.GetName()}: (ontheboard/indeck/inhand/indiscard-pile) {Support.CountCards<OnTheBoardFaceUp>(cards_player1) + Support.CountCards<OnTheBoardFaceDown>(cards_player1) + Support.CountCards<IsTapped>(cards_player1)}/{Support.CountCards<InTheDeck>(cards_player1)}/{Support.CountCards<InTheHand>(cards_player1)}/{Support.CountCards<OnTheDisposedPile>(cards_player1)}");
        Console.WriteLine($"Player {this.Player1.GetName()} on the board: {{");
        Console.WriteLine($"\t Face-Up: " + Support.CardIdsHumanFormatted<OnTheBoardFaceUp>(cards_player1));
        Console.WriteLine($"\t Face-Down: " + Support.CardIdsHumanFormatted<OnTheBoardFaceDown>(cards_player1));
        Console.WriteLine($"\t Tapped: " + Support.CardIdsHumanFormatted<IsTapped>(cards_player1));
        Console.WriteLine($"}}");
        Console.WriteLine($"Player {this.Player1.GetName()} in deck: " + Support.CardIdsHumanFormatted<InTheDeck>(cards_player1));
        Console.WriteLine($"Player {this.Player1.GetName()} in hand: " + Support.CardIdsHumanFormatted<InTheHand>(cards_player1));
        Console.WriteLine($"Player {this.Player1.GetName()} on the discard-pile: " + Support.CardIdsHumanFormatted<OnTheDisposedPile>(cards_player1));

        Console.WriteLine("------------------------------------");

        List<Card> cards_player2 = this.Player2.GetCards();
        Console.WriteLine($"Player {this.Player2.GetName()}: (ontheboard/indeck/inhand/indiscard-pile) {Support.CountCards<OnTheBoardFaceUp>(cards_player2) + Support.CountCards<OnTheBoardFaceDown>(cards_player2) + Support.CountCards<IsTapped>(cards_player2)}/{Support.CountCards<InTheDeck>(cards_player2)}/{Support.CountCards<InTheHand>(cards_player2)}/{Support.CountCards<OnTheDisposedPile>(cards_player2)}");
        Console.WriteLine($"Player {this.Player2.GetName()} on the board: {{");
        Console.WriteLine($"\t Face-Up: " + Support.CardIdsHumanFormatted<OnTheBoardFaceUp>(cards_player2));
        Console.WriteLine($"\t Face-Down: " + Support.CardIdsHumanFormatted<OnTheBoardFaceDown>(cards_player2));
        Console.WriteLine($"\t Tapped: " + Support.CardIdsHumanFormatted<IsTapped>(cards_player2));
        Console.WriteLine($"}}");
        Console.WriteLine($"Player {this.Player2.GetName()} in deck: " + Support.CardIdsHumanFormatted<InTheDeck>(cards_player2));
        Console.WriteLine($"Player {this.Player2.GetName()} in hand: " + Support.CardIdsHumanFormatted<InTheHand>(cards_player2));
        Console.WriteLine($"Player {this.Player2.GetName()} on the discard-pile: " + Support.CardIdsHumanFormatted<OnTheDisposedPile>(cards_player2));

        Console.WriteLine("==== END Current situation\n");
    }

    public Player GetPlayerById(Guid playerId)
    {
        return playerId == this.Player1.Id
            ? Player1
            : playerId == this.Player2.Id
            ? Player2
            : throw new ArgumentException($"Invalid playerId. Value: {playerId}");
    }

    public void DrawInitialCards()
    {
        for (int cnt = 0; cnt < 7; cnt++)
        {
            this.Player1.DrawCard();
            this.Player2.DrawCard();
        }

        this.LogCurrentSituation();
    }

    private void SwapPlayer()
    {
        if (this.CurrentPlayer.GetName() == this.Player1.GetName())
        {
            this.CurrentPlayer = this.Player2;
            this.OpponentPlayer = this.Player1;
        }
        else
        {
            this.CurrentPlayer = this.Player1;
            this.OpponentPlayer = this.Player2;
        }
    }
}