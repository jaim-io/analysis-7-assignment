using TheCardGame.Cards.Events;
using TheCardGame.Common.Models;
using TheCardGame.Effects.Types;
using TheCardGame.Games;

namespace TheCardGame.Cards.States;

public class InTheHand
    : CardState
{
    public InTheHand(CardState state)
        : base(state.Card)
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
        this.card.State = new OnTheDisposedPile(this);
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
        if (this.card.State is InTheHand)
        {
            GameBoard.GetInstance().AddObserver(this.card);
            this.card.State = new OnTheBoardFaceUp(this);
        }
        return true;
    }
}