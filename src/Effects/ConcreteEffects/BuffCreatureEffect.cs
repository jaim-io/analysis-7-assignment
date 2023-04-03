using TheCardGame.Cards;
using TheCardGame.Cards.Events;
using TheCardGame.Common.Models;
using TheCardGame.Effects.States;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Effects.ConcreteEffects;

public class BuffCreatureEffect : Effect
{
    private uint _attackOffset { get; init; }
    private uint _defenseOffset { get; init; }
    private uint _currentTurn;
    public BuffCreatureEffect(
        string name,
        string description,
        uint attackOffset,
        uint defenseOffset,
        Func<List<Entity>>? getPreDeterminedTargets = null,
        Func<bool>? duration = null)
        : base(new OnRevealEffect(), name, description, getPreDeterminedTargets, duration)
    {
        this._attackOffset = attackOffset;
        this._defenseOffset = defenseOffset;
    }

    public override void Trigger()
    {
        this.State = new Active(this);
        GameBoard.GetInstance().AddObserver(this);
        this._currentTurn = GameBoard.GetInstance().Turn;

        _userInvokedTargets.ForEach(entity =>
        {
            if (entity is CreatureCard creature)
            {
                creature.AddObserver(this);
                // Creature.Attack += _attackOffset;
                // Creature.Defense += _defenseOffset;
            }
        });
    }

    public override void StartOfTurn(StartOfTurnEvent eventInfo)
    {
        if (GameBoard.GetInstance().Turn >= this._currentTurn + 1)
        {
            this.State = new Used(this);
            GameBoard.GetInstance().RemoveObserver(this);
            _userInvokedTargets.ForEach(entity =>
            {
                if (entity is CreatureCard creature)
                {
                    creature.RemoveObserver(this);
                    // Creature.Attack -= Creature.InitialAttack
                    // Creature.Defense -= Creature.InitialDefense;
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
                // Creature.Attack -= Creature.InitialAttack
                // Creature.Defense -= Creature.InitialDefense;
            }
        });
    }
}