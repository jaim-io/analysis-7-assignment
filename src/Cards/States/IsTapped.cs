namespace TheCardGame.Cards.States;

public class IsTapped
    : CardState
{
    public IsTapped(CardState state) 
        : base(state.Card)
    {
    }

    public override int givesEnergyLevel()
    {
        return this.card.getEnergyLevel();
    }

    public override void onEndTurn()
    {
        this.card.State = new OnTheBoard(this);
    }
}