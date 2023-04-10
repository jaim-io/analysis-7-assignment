using TheCardGame.Cards.Events;
using TheCardGame.Effects.Types;
using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class OnTheBoardFaceDown
    : CardState
{
    public OnTheBoardFaceDown(Card card)
        : base(card)
    {
    }

    public override void TurnFaceUp()
    {
        this.card.State = new OnTheBoardFaceUp(this.card);
    }

    public override bool Dispose()
    {
        var disposedEvent = new CardDisposedEvent(this.card);
        foreach (var obs in this.card.Observers)
        {
            obs.CardDisposed(disposedEvent);
        }

        this.card.Effects.ForEach(e => e.Dispose());

        GameBoard.GetInstance().RemoveObserver(this.card);
        this.card.RemoveAllObservers();

        this.card.State = new OnTheDisposedPile(this.card);

        return true;
    }
}