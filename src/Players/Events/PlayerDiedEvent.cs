namespace TheCardGame.Players.Events;

public record PlayerDiedEvent(
    string PlayerName,
    int Health,
    string Reason);