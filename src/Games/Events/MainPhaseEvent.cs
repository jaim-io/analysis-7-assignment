namespace TheCardGame.Games.Events;

public record MainPhaseEvent(
    uint TurnNumber,
    Guid PlayerId);