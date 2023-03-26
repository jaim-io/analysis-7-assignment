using TheCardGame.Players.Events;

namespace TheCardGame.Players;

public abstract class PlayerObserver
{
    public abstract void PlayerDied(PlayerDiedEvent eventInfo);
}