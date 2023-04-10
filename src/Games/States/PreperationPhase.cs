// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Cards;
using TheCardGame.Common.Models;
using TheCardGame.Games;
using TheCardGame.Utils;

namespace TheCardGame.Games.States;

public class PreperationPhase : GameState
{
    public PreperationPhase(GameBoard game)
        : base(game)
    {
        Console.WriteLine($"[GameState] changed to PreperationPhase");
    }

    public override void ToDrawingPhase()
    {
        this.game.State = new DrawingPhase(this.game);
    }

    public override void ActivateEffect(Guid playerId, string cardId, string effectName, List<Entity>? targets = null)
    {
        var player = GameBoard.GetInstance().GetPlayerById(playerId);

        (Card card, int _) = Support.FindCard(player.GetCards(), cardId);
        card?.ActivateEffect(effectName, targets);
    }
}