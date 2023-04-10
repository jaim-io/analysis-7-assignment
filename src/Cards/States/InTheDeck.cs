// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Cards.States;

public class InTheDeck
    : CardState
{
    public InTheDeck(Card card)
        : base(card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override bool IsNotYetInTheGame()
    {
        return true;
    }

    public override bool OnDraw()
    {
        this.card.State = new InTheHand(this.card);
        return true;
    }
}