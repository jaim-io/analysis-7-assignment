using TheCardGame.Games.Events;

namespace TheCardGame.Games;

public interface IGameBoardObserver
{
    void PreparationPhase(PreparationPhaseEvent eventInfo);
    void MainPhase(MainPhaseEvent eventInfo);
    void EndPhase(EndPhaseEvent eventInfo);
}