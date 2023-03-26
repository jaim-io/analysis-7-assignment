namespace TheCardGame.Cards.States;

public class IsDefending
    : CardState
{
    public IsDefending(CardState state)
        : base(state.Card)
    {
    }

    public override int GivesEnergyLevel()
    {
        return 0;
    }

    public override void OnEndTurn()
    {
        this.card.State = new OnTheBoard(this);
    }

    public override (bool, int) AbsorbAttack(int iAttackValue)
    {
        int defenseValue = this.card.GetDefenseValue();
        int attackValueLeft = iAttackValue - defenseValue;
        int defenseValueLeft = this.card.SubtractDefenseValue(iAttackValue);
        Console.WriteLine($"Card '{this.card.GetId()}' with defense-value {defenseValue} absorbed attack-value {iAttackValue}. Attack value left: {attackValueLeft}");
        if (defenseValueLeft <= 0)
        {
            this.card.State = new OnTheDisposedPile(this);
        }
        return (true, attackValueLeft);
    }
}