namespace TheCardGame.Cards.States;

public class InTheHand
    : CardState
{
    public InTheHand(CardState state)
        : base(state.Card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override bool IsInTheHand()
    {
        return true;
    }

    public override bool Dispose()
    {
        this.card.State = new OnTheDisposedPile(this);
        return true;
    }

    public override bool OnPlay()
    {
        this.card.PreRevealEffect?.Activate(); // Only catch is now that PreRevealEffect cannot have targets
        if (this.card.State is InTheHand)
        {
            this.card.State = new OnTheBoardFaceUp(this);
            // Manually activate effect from program.cs
        }
        return true;
    }
}