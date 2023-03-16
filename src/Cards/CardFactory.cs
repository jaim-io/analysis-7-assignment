namespace TheCardGame.Cards;

public abstract class CardFactory
{
    public abstract LandCard createLandCard(string cardId);
    public abstract SpellCard createSpellCard(string cardId);
    public abstract CreatureCard createCreatureCard(string cardId);
}