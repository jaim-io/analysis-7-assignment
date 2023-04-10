// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Players.Events;

public record PlayerDiedEvent(
    string PlayerName,
    int Health,
    string Reason);