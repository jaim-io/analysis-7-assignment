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

    public override int GetEnergyLevel()
    {
        return this._energyLevel;
    }

    public override void TapEnergy()
    {
        this.State.TapEnergy();
    }
}