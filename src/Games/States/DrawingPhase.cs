using TheCardGame.Games;
using TheCardGame.Players;

public class DrawingPhase : GameState
{
    public DrawingPhase(GameBoard game)
        : base(game)
    {
    }

    public override void NextState()
    {
        this.game.State = new MainPhase(this.game);
    }

    public override bool TakeCard()
    {
        Player currentPlayer = this.game.CurrentPlayer;
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