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
        this.card.State = new OnTheBoard(this);
        this.card.ActivateEffect(); // => Sleight of hand change state here?
        // ^^^^^ => this.card.ActivatePreRevealEffect() => card.State = new OnTheBoardFaceDown(this);
        // conditional = () => ...
        // if this.card.State is OnTheBoard? => FaceUp
        // else we can assume the effect changed it
        // OnTheBoardFaceDown => OnTheBoardFaceUp
        return true;
    }
}