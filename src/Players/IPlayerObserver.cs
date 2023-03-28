using TheCardGame.Players.Events;

namespace TheCardGame.Players;

public interface IPlayerObserver
{
    void PlayerDied(PlayerDiedEvent eventInfo);
}