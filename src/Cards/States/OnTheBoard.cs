namespace TheCardGame.Cards.States;

public class OnTheBoard
    : CardState
{
    public OnTheBoard(CardState state)
        : base(state.Card)
    {
    }

    public override void tapEnergy()
    {
        this.card.State = new IsTapped(this);
    }

    public override void goDefending()
    {
        this.card.State = new IsDefending(this);
    }

    public override void goAttacking()
    {
        this.card.State = new IsAttacking(this);
    }

    public override int givesEnergyLevel()
    {
        return 0;
    }

    public override bool canBePlayed() { return true; }
}