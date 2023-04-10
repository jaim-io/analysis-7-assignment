// Jamey Schaap 0950044
// Vincent de Gans 1003196

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