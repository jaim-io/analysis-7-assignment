// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Games.Events;

namespace TheCardGame.Games;

public interface IGameBoardObserver
{
    void PreparationPhase(PreparationPhaseEvent eventInfo);
    void MainPhase(MainPhaseEvent eventInfo);
    void EndPhase(EndPhaseEvent eventInfo);
}