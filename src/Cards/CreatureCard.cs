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
        List<Colour> colours,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null)
        : base(cardId, colours, effects)
    {
        this._initialAttackValue = attackValue;
        this._actualAttackValue = attackValue;
        this._initialDefenseValue = defenseValue;
        this._actualDefenseValue = defenseValue;
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

    public override int SubtractDefenseValue(int value)
    {
        this._initialDefenseValue = this._initialDefenseValue - value;
        return this._initialDefenseValue;
    }

    public override int GetInitialAttackValue() { return this._initialAttackValue; }
    public override int GetAttackValue() { return this._actualAttackValue; }
    public override int GetInitialDefenseValue() { return this._initialDefenseValue; }
}