namespace TheCardGame.Players;

public abstract class PlayerObserver
{
    public abstract void playerDied(PlayerDiedEvent eventInfo);
}