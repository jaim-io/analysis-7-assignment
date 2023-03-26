using TheCardGame.Cards.Colours;

namespace TheCardGame.Cards;

public abstract class CardFactory
{
    public abstract LandCard CreateLandCard(string cardId, Colour colour);
    public abstract SpellCard CreateSpellCard(string cardId, Colour colour);
    public abstract CreatureCard CreateCreatureCard(string cardId, Colour colour, int attackValue, int defenseValue);
}