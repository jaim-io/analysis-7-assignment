using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Demos;

public class DemoLandCard
    : LandCard
{
    public DemoLandCard(string cardId, List<Colour> colours)
        : base(cardId, colours)
    {
    }
}

public class DemoSpellCard
    : SpellCard
{
    public DemoSpellCard(
        string cardId,
        List<Colour> colours,
        List<Effect>? effects = null)
        : base(cardId, colours, effects)
    {
    }
}

public class DemoCreatureCard
    : CreatureCard
{
    public DemoCreatureCard(
        string cardId,
        List<Colour> colours,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null)
        : base(cardId, colours, attackValue, defenseValue, effects)
    {
    }
}