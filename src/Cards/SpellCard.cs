using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class SpellCard : Card
{
    public SpellCard(
        string cardId,
        Colour colour,
        List<Effect>? effects)
        : base(cardId, colour, effects)
    {
    }
}