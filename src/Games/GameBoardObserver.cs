using TheCardGame.Games.Events;

namespace TheCardGame.Games;

public interface GameBoardObserver
{
    void StartOfTurn(StartOfTurnEvent eventInfo);
    void EndOfTurn(EndOfTurnEvent eventInfo);
}