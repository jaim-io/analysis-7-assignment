using TheCardGame.Cards;
using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoCardFactory : CardFactory
{
    public override LandCard CreateLandCard(string cardId, Colour colour) => new DemoLandCard(cardId, colour);

    public override SpellCard CreateSpellCard(string cardId, Colour colour) => new DemoSpellCard(cardId, colour);

    public override CreatureCard CreateCreatureCard(
        string cardId,
        Colour colour,
        int attackValue,
        int defenseValue) => new DemoCreatureCard(cardId, colour, attackValue, defenseValue);
}