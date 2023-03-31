using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class CardFactory
{
    public abstract LandCard CreateLandCard(string cardId, ICollection<Colour> colours);
    public abstract SpellCard CreateSpellCard(string cardId, ICollection<Colour> colours, List<Effect>? effects = null);
    public abstract CreatureCard CreateCreatureCard(string cardId, ICollection<Colour> colours, int attackValue, int defenseValue, List<Effect>? effects = null);
}