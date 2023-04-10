// Jamey Schaap 0950044
// Vincent de Gans 1003196

using TheCardGame.Cards;
using TheCardGame.Cards.Events;
using TheCardGame.Cards.States;
using TheCardGame.Common.Models;
using TheCardGame.Effects.Types;
using TheCardGame.Games;
using TheCardGame.Games.Events;

namespace TheCardGame.Effects.ConcreteEffects.MultiPurpose;

public class ModifyAttackDamageEffect : Effect
{
    private List<Type> _creatureTypes;
    private Func<int, int> _attackModifier;

    public ModifyAttackDamageEffect(
        string name,
        Func<int, int> attackModifier,
        List<Type> creatureStates)
        : base(
            new OnRevealEffect(),
            name,
            "Modifies the attack damage for all creatures, matching the provided creature states.",
            null)
    {
        this._attackModifier = attackModifier;
        this._creatureTypes = creatureStates;
    }

    public override void Apply()
    {
        Console.WriteLine($"[ModifyAttackDamage] has been activated for the current player. >>(In the demo this halves creature damage)<<");

        GameBoard.GetInstance().AddObserver(this);
        GameBoard.GetInstance().CurrentPlayer.GetCards().ForEach(card =>
        {
            if (card is CreatureCard creature && _creatureTypes.Contains(creature.State.GetType()))
            {
                creature.ModifyAttackValue(this._attackModifier);
            }
        });
    }

    public override void PreparationPhase(PreparationPhaseEvent eventInfo)
    {
        Console.WriteLine($"[ModifyAttackDamage] has been activated for the current player. >>(In the demo this halves creature damage)<<");

        GameBoard.GetInstance().CurrentPlayer.GetCards().ForEach(card =>
        {
            if (card is CreatureCard creature && _creatureTypes.Contains(creature.State.GetType()))
            {
                creature.ModifyAttackValue(this._attackModifier);
            }
        });
    }

    public override void EndPhase(EndPhaseEvent eventInfo)
    {
        Console.WriteLine($"[ModifyAttackDamage] has been reset for the current player. >>(In the demo this halves creature damage)<<");

        GameBoard.GetInstance().CurrentPlayer.GetCards().ForEach(card =>
        {
            if (card is CreatureCard creature && _creatureTypes.Contains(creature.State.GetType()))
            {
                creature.ResetAttackValue();
            }
        });
    }
}