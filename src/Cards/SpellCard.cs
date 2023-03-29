using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class SpellCard : Card
{
    public SpellCard(
        string cardId,
        Colour colour,
        Effect? onRevealEffect = null,
        Effect? preRevealEffect = null)
        : base(cardId, colour, onRevealEffect, preRevealEffect)
    {
    }
}