using TheCardGame.Cards.Colours;

namespace TheCardGame.Cards;

public abstract class ArtefactCard : Card
{
    protected ArtefactCard(string cardId, int cost)
        : base(cardId, new() { new Colourless(cost) })
    {
    }
}