namespace TheCardGame.Cards.States;

public class IsDefending
    : CardState
{
    public IsDefending(CardState state)
        : base(state.Card)
    {
    }

    public override int givesEnergyLevel()
    {
        return 0;
    }

    public override void onEndTurn()
    {
        this.card.State = new OnTheBoard(this);
    }

    public override (bool, int) absorbAttack(int iAttackValue)
    {
        int defenseValue = this.card.getDefenseValue();
        int attackValueLeft = iAttackValue - defenseValue;
        int defenseValueLeft = this.card.subtractDefenseValue(iAttackValue);
        System.Console.WriteLine($"Card '{this.card.getId()}' with defense-value {defenseValue} absorbed attack-value {iAttackValue}. Attack value left: {attackValueLeft}");
        if (defenseValueLeft <= 0)
        {
            this.card.State = new OnTheDisposedPile(this);
        }
        return (true, attackValueLeft);
    }
}