using TheCardGame.Games.Events;

namespace TheCardGame.Games;

public abstract class GameBoardObserver
{
    public abstract void StartOfTurn(StartOfTurnEvent eventInfo);
    public abstract void EndOfTurn(EndOfTurnEvent eventInfo);
}