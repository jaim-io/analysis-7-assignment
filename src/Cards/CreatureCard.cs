using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class CreatureCard
    : Card
{
    /* Used to attack opponenent (decrease opponent lifePoint) or for defense.
    
    */
    private int _initialAttackValue; /* The attackValue defined on this card*/
    private int _actualAttackValue; /* The attackValue for this attack after defense cards came into action */
    private int _initialDefenseValue;
    private int _actualDefenseValue;

    public CreatureCard(
        string cardId,
        Colour colour,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null)
        : base(cardId, colour, effects)
    {
        this._initialAttackValue = attackValue;
        this._actualAttackValue = attackValue;
        this._initialDefenseValue = defenseValue;
        this._actualDefenseValue = defenseValue;
    }

    public void DecreaseActualAttackValue(int iNumber)
    {
        this._actualAttackValue -= iNumber;
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
        this._initialDefenseValue = this._initialDefenseValue - iAttackValue;
        return this._initialDefenseValue;
    }

    public override int GetInitialAttackValue() { return this._initialAttackValue; }
    public override int GetActualAttackValue() { return this._actualAttackValue; }
    public override int GetDefenseValue() { return this._initialDefenseValue; }
}