namespace TheCardGame.Cards.States;

public class OnTheDisposedPile
    : CardState
{
    public OnTheDisposedPile(CardState state)
        : base(state.Card)
    {
    }

    public override int givesEnergyLevel()
    {
        return 0;
    }
}