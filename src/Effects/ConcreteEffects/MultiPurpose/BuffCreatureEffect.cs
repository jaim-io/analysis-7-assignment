using TheCardGame.Cards;
using TheCardGame.Cards.Events;
using TheCardGame.Effects.States;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Effects.ConcreteEffects.MultiPurpose;

public class BuffCreatureEffect : Effect
{
    private int _attackOffset { get; init; }
    private int _defenseOffset { get; init; }
    private uint _startingTurn;
    private uint _amountOfTurns;
    public BuffCreatureEffect(
        string name,
        string description,
        int attackOffset,
        int defenseOffset,
        uint amountOfTurns)
        : base(new OnRevealEffect(), name, description, null, null)
    {
        this._attackOffset = attackOffset;
        this._defenseOffset = defenseOffset;
        this._amountOfTurns = amountOfTurns;
    }

    public override void Trigger()
    {
        this.State = new Active(this);
        GameBoard.GetInstance().AddObserver(this);
        this._startingTurn = GameBoard.GetInstance().Turn;

        _userInvokedTargets.ForEach(entity =>
        {
            if (entity is CreatureCard creature)
            {
                creature.AddObserver(this);
                creature.ModifyAttackValue(this._attackOffset);
                creature.ModifyDefenceValue(this._defenseOffset);
            }
        });
    }

    public override void StartOfTurn(StartOfTurnEvent eventInfo)
    {
        if (GameBoard.GetInstance().Turn >= this._startingTurn + this._amountOfTurns)
        {
            this.State = new Used(this);
            GameBoard.GetInstance().RemoveObserver(this);
            _userInvokedTargets.ForEach(entity =>
            {
                if (entity is CreatureCard creature)
                {
                    creature.RemoveObserver(this);
                    creature.ResetAttackValue();
                    creature.ResetDefenceValue();
                }
            });
        }
    }
    public override void CardDisposed(CardDisposedEvent eventInfo)
    {
        this.State = new Used(this);
        GameBoard.GetInstance().RemoveObserver(this);
        _userInvokedTargets.ForEach(entity =>
        {
            if (entity is CreatureCard creature)
            {
                creature.RemoveObserver(this);
                creature.ResetAttackValue();
                creature.ResetDefenceValue();
            }
        });
    }
}