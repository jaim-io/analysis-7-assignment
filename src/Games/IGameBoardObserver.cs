using TheCardGame.Games.Events;

namespace TheCardGame.Games;

public interface IGameBoardObserver
{
    void StartOfTurn(StartOfTurnEvent eventInfo);
    void EndOfTurn(EndOfTurnEvent eventInfo);
}