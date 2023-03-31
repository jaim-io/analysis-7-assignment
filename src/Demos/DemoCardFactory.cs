using TheCardGame.Cards;
using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Demos;

public class DemoCardFactory : CardFactory
{
    public override LandCard CreateLandCard(
        string cardId,
        ICollection<Colour> colours) => new DemoLandCard(cardId, colours);

    public override SpellCard CreateSpellCard(
        string cardId,
        ICollection<Colour> colours,
        List<Effect>? effects = null) => new DemoSpellCard(cardId, colours, effects);

    public override CreatureCard CreateCreatureCard(
        string cardId,
        ICollection<Colour> colours,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null) => new DemoCreatureCard(cardId, colours, attackValue, defenseValue, effects);
}