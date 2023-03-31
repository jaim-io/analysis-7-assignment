namespace TheCardGame.Cards.States;

public class IsTapped
    : CardState
{
    public IsTapped(CardState state) 
        : base(state.Card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return this.card.GetEnergyLevel();
    }

    public override void OnStartTurn()
    {
        this.card.State = new OnTheBoardFaceUp(this);
    }
}