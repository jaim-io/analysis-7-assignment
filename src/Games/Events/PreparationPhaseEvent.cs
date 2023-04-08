namespace TheCardGame.Games.Events;

public record PreparationPhaseEvent(
    uint TurnNumber,
    Guid PlayerId);