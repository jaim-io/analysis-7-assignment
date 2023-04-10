// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Cards.Events;
using TheCardGame.Common.Models;
using TheCardGame.Effects.Types;
using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class InTheHand
    : CardState
{
    public InTheHand(Card card)
        : base(card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override bool IsInTheHand()
    {
        return true;
    }

    public override bool Dispose()
    {
        var disposedEvent = new CardDisposedEvent(this.card);
        foreach (var obs in this.card.Observers)
        {
            obs.CardDisposed(disposedEvent);
        }
        this.card.State = new OnTheDisposedPile(this.card);
        return true;
    }


    public override void ActivateEffect(string name, List<Entity>? targets)
    {
        var effect = this.card.Effects.FirstOrDefault(e => e.Name == name);

        if (effect == null)
        {
            throw new ArgumentException($"Effect with name {name} does not exist on card {this.card.GetId()}");
        }

        if (effect.Type is PreRevealEffect)
        {
            effect.Activate(targets);
        }
    }

    public override bool OnPlay()
    {
        this.card.Effects.ForEach(e =>
        {
            if (e.Type is PreRevealEffect)
            {
                e.ActivateWithoutStack();
            }
        });

        if (this.card.State is InTheHand)
        {
            GameBoard.GetInstance().AddObserver(this.card);
            this.card.State = new OnTheBoardFaceUp(this.card);
        }
        return true;
    }
}