// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Players.Events;

namespace TheCardGame.Players;

public interface IPlayerObserver
{
    void PlayerDied(PlayerDiedEvent eventInfo);
}