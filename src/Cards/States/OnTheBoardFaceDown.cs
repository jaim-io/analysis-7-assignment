using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class OnTheBoardFaceDown
    : CardState
{
    public OnTheBoardFaceDown(CardState state)
        : base(state.Card)
    {
    }

    public override void TurnFaceUp()
    {
        this.card.State = new OnTheBoardFaceUp(this);
    }

    public override bool Dispose()
    {
        this.card.State = new OnTheDisposedPile(this);
        GameBoard.GetInstance().RemoveObserver(this.card);
        // this.card.OnRevealEffect?.Dispose();
        return true;
    }
}