using TheCardGame.Games;
using TheCardGame.Games.Events;
using TheCardGame.Players;
using TheCardGame.Players.Constraints;

public class DrawingPhase : GameState
{
    public DrawingPhase(GameBoard game)
        : base(game)
    {
    }

    public override void ToMainPhase()
    {
        this.game.State = new MainPhase(this.game);

        var mainPhaseEvent = new MainPhaseEvent(this.game.Turn);
        // Deep clone _observers so observers are able to remove themself safely from the _observer list.
        this.game.Observers
            .ConvertAll(o => o)
            .ForEach(o => o.MainPhase(mainPhaseEvent));
    }

    public override bool TakeCard()
    {
        Player currentPlayer = this.game.CurrentPlayer;
        var skipDrawing = currentPlayer.Constraints.Any(c => c is SkipDrawing);

        if (skipDrawing)
        {
            Console.WriteLine($"{currentPlayer.GetName()} skipped their drawing phase.");
            return true;
        }

        var card = this.game.CurrentPlayer.DrawCard();
        if (card == null)
        {
            Console.WriteLine($"{currentPlayer.GetName()} could not take card.");
            return false;
        }
        else
        {
            Console.WriteLine($"{currentPlayer.GetName()} took card {card.GetId()} from deck into hand.");
            return true;
        }
    }

    public override bool DrawCard(string cardId)
    {
        Player currentPlayer = this.game.CurrentPlayer;

        var skipDrawing = currentPlayer.Constraints.Any(c => c is SkipDrawing);

        if (skipDrawing)
        {
            Console.WriteLine($"{currentPlayer.GetName()} cannot draw any cards this turn.");
            return true;
        }

        var card = this.game.CurrentPlayer.DrawCard(cardId);
        if (card is null)
        {
            Console.WriteLine($"{currentPlayer.GetName()} Didn't draw card {cardId}: Not in his hand.");
            return false;
        }

        Console.WriteLine($"{currentPlayer.GetName()} draw card {card.GetId()}.");
        return true;
    }
}