using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class CardFactory
{
    public abstract LandCard CreateLandCard(string cardId, Colour colour);
    public abstract SpellCard CreateSpellCard(string cardId, Colour colour, Effect? onRevealEffect = null, Effect? preRevealEffect = null);
    public abstract CreatureCard CreateCreatureCard(string cardId, Colour colour, int attackValue, int defenseValue, Effect? onRevealEffect = null, Effect? preRevealEffect = null);
}