using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Demos;

public class DemoCardFactory : CardFactory
{
    public override LandCard CreateLandCard(
        string cardId,
        Colour colour) => new DemoLandCard(cardId, colour);

    public override SpellCard CreateSpellCard(
        string cardId,
        Colour colour,
        List<Effect>? effects = null) => new DemoSpellCard(cardId, colour, effects);

    public override CreatureCard CreateCreatureCard(
        string cardId,
        Colour colour,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null) => new DemoCreatureCard(cardId, colour, attackValue, defenseValue, effects);
}