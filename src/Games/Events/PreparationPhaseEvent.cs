// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Games.Events;

public record PreparationPhaseEvent(
    uint TurnNumber,
    Guid PlayerId);