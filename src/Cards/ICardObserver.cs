using TheCardGame.Cards.Events;

namespace TheCardGame.Cards;

public interface ICardObserver
{
    void CardDisposed(CardDisposedEvent eventInfo);
}