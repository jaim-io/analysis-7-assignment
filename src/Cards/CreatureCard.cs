namespace TheCardGame.Cards;

public abstract class CreatureCard 
    : Card
{
    /* Used to attack opponenent (decrease opponent lifePoint) or for defense.
    
    */
    private int initialAttackValue = 0; /* The attackValue defined on this card*/
    private int actualAttackValue = 0; /* The attackValue for this attack after defense cards came into action */
    private int defenseValue = 0;

    public CreatureCard(string cardId) 
        : base(cardId)
    {
    }

    public void DecreaseActualAttackValue(int iNumber)
    {
        this.actualAttackValue -= iNumber;
    }

    public override void GoDefending()
    {
        this.State.GoDefending();
    }

    public override void PeformAttack()
    {
        this.State.PeformAttack();
    }

    public override void GoAttacking()
    {
        this.State.GoAttacking();
    }

    public override int SubtractDefenseValue(int iAttackValue)
    {
        this.defenseValue = this.defenseValue - iAttackValue;
        return this.defenseValue;
    }

    public override int GetInitialAttackValue() { return this.initialAttackValue; }
    public override int GetActualAttackValue() { return this.actualAttackValue; }
    public override int GetDefenseValue() { return this.defenseValue; }
}