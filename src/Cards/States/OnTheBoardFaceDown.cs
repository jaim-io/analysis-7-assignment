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
        // Manually activate effect from program.cs
    }

    public override bool Dispose()
    {
        this.card.State = new OnTheDisposedPile(this);
        // this.card.OnRevealEffect?.Dispose();
        return true;
    }
}