using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Demos;

public class DemoLandCard
    : LandCard
{
    public DemoLandCard(string cardId, Colour colour)
        : base(cardId, colour)
    {
    }
}

public class DemoSpellCard
    : SpellCard
{
    public DemoSpellCard(
        string cardId,
        Colour colour,
        List<Effect>? effects = null)
        : base(cardId, colour, effects)
    {
    }
}

public class DemoCreatureCard
    : CreatureCard
{
    public DemoCreatureCard(
        string cardId,
        Colour colour,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null)
        : base(cardId, colour, attackValue, defenseValue, effects)
    {
    }
}