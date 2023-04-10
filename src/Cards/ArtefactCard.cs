// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class ArtefactCard : Card
{
    protected ArtefactCard(
        string cardId,
        int cost,
        List<Effect>? effects = null)
        : base(cardId, new() { new Colourless(cost) }, effects)
    {
    }
}