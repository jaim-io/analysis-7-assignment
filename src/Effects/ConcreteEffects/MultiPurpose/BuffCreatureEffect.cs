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
        this._amountOfTurns = amountOfTurns - 1;
    }

    public override void Trigger()
    {
        GameBoard.GetInstance().AddObserver(this);
        this._startingTurn = GameBoard.GetInstance().Turn;

        _userInvokedTargets.ForEach(entity =>
        {
            if (entity is CreatureCard creature)
            {
                creature.AddObserver(this);
                creature.ModifyAttackValue((x) => x + this._attackOffset);
                creature.ModifyDefenceValue((x) => x + this._defenseOffset);
            }
        });
    }

    public override void EndPhase(EndPhaseEvent eventInfo)
    {
        if (GameBoard.GetInstance().Turn >= this._startingTurn + this._amountOfTurns)
        {
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
            this.Dispose();
            this.Owner!.Dispose();
        }
    }

    public override void CardDisposed(CardDisposedEvent eventInfo)
    {
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
        this.Dispose();
    }
}