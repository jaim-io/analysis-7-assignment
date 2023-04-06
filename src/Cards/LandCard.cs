using TheCardGame.Cards.Colours;

namespace TheCardGame.Cards;

public abstract class LandCard
    : Card
{
    /* Provides the energy to play the other cards */
    private int _energyLevel = 1;

    public LandCard(string cardId, List<Colour> colours)
        : base(cardId, colours)
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