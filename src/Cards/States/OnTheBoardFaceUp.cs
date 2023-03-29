using TheCardGame.Common;

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
        this.card.OnRevealEffect?.Dispose();
        return true;
    }

    public override bool CanBePlayed() { return true; }

    public override void ActivateEffect(List<Entity>? targets)
    {
        this.card.OnRevealEffect?.Activate(targets);
    }
}