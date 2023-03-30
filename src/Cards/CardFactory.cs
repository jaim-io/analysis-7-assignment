using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class CardFactory
{
    public abstract LandCard CreateLandCard(string cardId, Colour colour);
    public abstract SpellCard CreateSpellCard(string cardId, Colour colour, List<Effect>? effects = null);
    public abstract CreatureCard CreateCreatureCard(string cardId, Colour colour, int attackValue, int defenseValue, List<Effect>? effects = null);
}