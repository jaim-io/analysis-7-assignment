namespace TheCardGame.Cards.States;

public class InTheDeck
    : CardState
{
    public InTheDeck(CardState state)
        : base(state.Card)
    {
    }

    public InTheDeck(Card card)
        : base(card)
    {
    }

    public override int givesEnergyLevel()
    {
        return 0;
    }

    public override bool isNotYetInTheGame()
    {
        return true;
    }

    public override bool onIsTaken()
    {
        this.card.State = new InTheHand(this);
        return true;
    }
}