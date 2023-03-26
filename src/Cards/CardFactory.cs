namespace TheCardGame.Cards;

public abstract class CardFactory
{
    public abstract LandCard CreateLandCard(string cardId);
    public abstract SpellCard CreateSpellCard(string cardId);
    public abstract CreatureCard CreateCreatureCard(string cardId);
}