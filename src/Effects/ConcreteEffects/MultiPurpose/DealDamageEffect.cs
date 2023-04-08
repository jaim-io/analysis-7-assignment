using TheCardGame.Cards;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Effects.Types;
using TheCardGame.Players;

namespace TheCardGame.Effects.ConcreteEffects.MultiPurpose;

public class DealDamageEffect : Effect
{
    public uint Damage { get; init; }
    public DealDamageEffect(
        string name,
        string description,
        uint damage,
        Func<List<Entity>>? getPreDeterminedTargets = null)
        : base(new OnRevealEffect(), name, description, getPreDeterminedTargets, null)
    {
        this.Damage = damage;
    }

    public override void Apply()
    {
        var preDeterminedTargets = this._getPreDeterminedTargets();
        var targets = this._userInvokedTargets.Concat(preDeterminedTargets);
        foreach (var target in targets)
        {
            if (target is Card card)
            {
                var initialState = card.State.GetType();

                card.GoDefending();
                card.State.AbsorbAttack((int)this.Damage);

                if (initialState == typeof(IsAttacking))
                {
                    card.GoAttacking();
                }
            }
            if (target is Player player)
            {
                player.DecreaseHealthValue((int)this.Damage);
            }
        }

        this.Dispose();
    }
}