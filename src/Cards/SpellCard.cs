using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class SpellCard : Card
{
    public SpellCard(
        string cardId,
        ICollection<Colour> colours,
        List<Effect>? effects)
        : base(cardId, colours, effects)
    {
    }
}