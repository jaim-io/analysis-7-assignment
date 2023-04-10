// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Cards.Events;

namespace TheCardGame.Cards;

public interface ICardObserver
{
    void CardDisposed(CardDisposedEvent eventInfo);
}