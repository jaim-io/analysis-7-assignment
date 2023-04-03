namespace TheCardGame.Cards.Events;

public record CardDisposedEvent(Card Card) : CardEvent;