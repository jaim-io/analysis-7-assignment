namespace TheCardGame.Players;

public class PlayerDiedEvent
{
    private string _playername;
    private int _health;
    private string _reason;
    public PlayerDiedEvent(string playername, int health, string reason)
    {
        this._playername = playername;
        this._health = health;
        this._reason = reason;
    }

    public string GetPlayerName()
    {
        return this._playername;
    }

    public int GetHealth()
    {
        return this._health;
    }
    public string GetReason()
    {
        return this._reason;
    }
}