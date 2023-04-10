// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Cards.States;

public class IsTapped
    : CardState
{
    public IsTapped(Card card) 
        : base(card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return this.card.GetEnergyLevel();
    }

    public override void OnStartTurn()
    {
        this.card.State = new OnTheBoardFaceUp(this.card);
    }
}