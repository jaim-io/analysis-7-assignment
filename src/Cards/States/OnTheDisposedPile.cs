namespace TheCardGame.Cards.States;

public class OnTheDisposedPile
    : CardState
{
    public OnTheDisposedPile(Card card)
        : base(card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }
}