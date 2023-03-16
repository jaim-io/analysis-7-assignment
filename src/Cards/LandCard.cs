namespace TheCardGame.Cards;

public abstract class LandCard
    : Card
{
    /* Provides the energy to play the other cards */
    private int _energyLevel = 0;

    public LandCard(string cardId)
        : base(cardId)
    {
    }

    public override int getEnergyLevel()
    {
        return this._energyLevel;
    }

    public override void tapEnergy()
    {
        this.State.tapEnergy();
    }
}