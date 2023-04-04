using TheCardGame.Cards.Colours;
using TheCardGame.Effects;

namespace TheCardGame.Cards;

public abstract class CreatureCard
    : Card
{
    /* Used to attack opponenent (decrease opponent lifePoint) or for defense. */
    private int _attackValue; /* The attackValue for this attack after defense cards came into action */
    private int _actualAttackValue; /* The attackValue defined on this card*/
    private int _defenseValue;
    private int _actualDefenseValue;

    public CreatureCard(
        string cardId,
        List<Colour> colours,
        int attackValue,
        int defenseValue,
        List<Effect>? effects = null)
        : base(cardId, colours, effects)
    {
        this._attackValue = attackValue;
        this._actualAttackValue = attackValue;
        this._defenseValue = defenseValue;
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
        this._defenseValue = this._defenseValue - value;
        return this._defenseValue;
    }

    public override int GetAttackValue() => this._attackValue;
    public override int GetDefenseValue() => this._defenseValue;

    public override void ModifyAttackValue(int offset) => this._attackValue += offset;
    public override void ModifyDefenceValue(int offset) => this._defenseValue += offset;

    public override void ResetAttackValue() => this._attackValue = this._actualAttackValue;
    public override void ResetDefenceValue() => this._defenseValue = this._actualDefenseValue;
}