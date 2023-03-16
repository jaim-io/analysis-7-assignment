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

    public void decreaseActualAttackValue(int iNumber)
    {
        this.actualAttackValue -= iNumber;
    }

    public override void goDefending()
    {
        this.State.goDefending();
    }

    public override void peformAttack()
    {
        this.State.peformAttack();
    }

    public override void goAttacking()
    {
        this.State.goAttacking();
    }

    public override int subtractDefenseValue(int iAttackValue)
    {
        this.defenseValue = this.defenseValue - iAttackValue;
        return this.defenseValue;
    }

    public override int getInitialAttackValue() { return this.initialAttackValue; }
    public override int getActualAttackValue() { return this.actualAttackValue; }
    public override int getDefenseValue() { return this.defenseValue; }
}