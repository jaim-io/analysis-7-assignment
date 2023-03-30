using TheCardGame.Games;

public abstract class GameState
{
    protected GameBoard game;

    public GameState(GameBoard game)
    {
        this.game = game;
    }

    public virtual void NextState() { }
    public virtual bool TakeCard() {
        return false;
    }

    public virtual bool DrawCard(string cardId) {
        return false;
    }
}