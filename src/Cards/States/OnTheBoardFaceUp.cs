using TheCardGame.Common.Models;

namespace TheCardGame.Cards.States;

public class OnTheBoardFaceUp
    : CardState
{
    public OnTheBoardFaceUp(CardState state)
        : base(state.Card)
    {
    }

    public override void TapEnergy()
    {
        this.card.State = new IsTapped(this);
    }

    public override void GoDefending()
    {
        this.card.State = new IsDefending(this);
    }

    public override void GoAttacking()
    {
        this.card.State = new IsAttacking(this);
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override bool Dispose()
    {
        this.card.State = new OnTheDisposedPile(this);
        // this.card.OnRevealEffect?.Dispose(); => check
        return true;
    }

    public override bool CanBePlayed() { return true; }

    public override void ActivateEffect(string name, List<Entity>? targets)
    {
        var effect = this.card.Effects.FirstOrDefault(e => e.Name == name);
        effect?.Activate(targets);
    }
}