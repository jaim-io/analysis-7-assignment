// Jamey Schaap 0950044
// Vincent de Gans 1003196

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