using TheCardGame.Cards.Colours;

namespace TheCardGame.Cards;

public abstract class LandCard
    : Card
{
    /* Provides the energy to play the other cards */
    private int _energyLevel = 0;

    public LandCard(string cardId, Colour colour)
        : base(cardId, colour)
    {
    }

    public override int GetEnergyLevel()
    {
        return this._energyLevel;
    }

    public override void TapEnergy()
    {
        this.State.TapEnergy();
    }
}