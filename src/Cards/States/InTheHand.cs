namespace TheCardGame.Cards.States;

public class InTheHand
    : CardState
{
    public InTheHand(CardState state)
        : base(state.Card)
    {
    }

    public override int givesEnergyLevel()
    {
        return 0;
    }

    public override bool isInTheHand()
    {
        return true;
    }

    public override bool dispose()
    {
        this.card.State = new OnTheDisposedPile(this);
        return true;
    }

    public override bool onDraw()
    {
        this.card.State = new OnTheBoard(this);
        return true;
    }
}