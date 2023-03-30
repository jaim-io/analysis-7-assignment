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
        Player currentTurnPlayer = this.game.GetCurrentTurnPlayer();
        var card = this.game.CurrentTurnPlayer.DrawCard();
        if (card == null)
        {
            Console.WriteLine($"{this.game.CurrentTurnPlayer.GetName()} could not take card.");
            return false;
        }
        else
        {
            Console.WriteLine($"{game.CurrentTurnPlayer.GetName()} took card {card.GetId()} from deck into hand.");
            return true;
        }
    }

    public override bool DrawCard(string cardId)
    {
        Player currentTurnPlayer = this.game.GetCurrentTurnPlayer();
        var card = this.game.CurrentTurnPlayer.DrawCard(cardId);
        if (card is null)
        {
            Console.WriteLine($"{this.game.CurrentTurnPlayer.GetName()} Didn't draw card {cardId}: Not in his hand.");
            return false;
        }

        Console.WriteLine($"{this.game.CurrentTurnPlayer.GetName()} draw card {card.GetId()}.");
        return true;
    }
}