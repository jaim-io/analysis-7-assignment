using TheCardGame.Cards.Colours;

namespace TheCardGame.Cards;

public abstract class SpellCard : Card
{
    public SpellCard(string cardId, Colour colour)
        : base(cardId, colour)
    {
    }
}