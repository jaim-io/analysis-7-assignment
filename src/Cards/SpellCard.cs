using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class SpellCard : Card
{
    public SpellCard(
        string cardId,
        List<Colour> colours,
        List<Effect>? effects)
        : base(cardId, colours, effects)
    {
    }
}