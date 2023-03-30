using TheCardGame.Games;

namespace TheCardGame.Effects.ConcreteEffects;

public abstract class CounterEffect : Effect
{
    public CounterEffect(
        string name,
        string description,
        Func<bool>? condition = null)
        : base(name, description, null, condition)
    {
    }

    public override void Trigger()
    {
        GameBoard.GetInstance().Stack.Skip(1);
    }
}

/*
 He plays “Hidden danger” and leaves it on the ground as a covered
card (Effect: sleight of hand—> reveal this card at the beginning of the
next opponent's turn. 4 damages are dealt to all creatures on the board.
The opponent will skip the drawing phase. Discard this card after the
effect has been completed).


“known game” (sleight of hand—> reveal this card at the
beginning of the next opponent's attack. 4 damages are dealt to all
creatures that can attack now on the board. Discard this card after the
effect has been completed).

The creature has the effect that removes a random card from the
opponent's hand when it comes into play

Arnold interrupts the resolution of the effect by getting 5 energy
to cast a red spell that gives a temporary buff to the creature (till the
end of the turn it will add +5/+3 to the target creature).

The artefact effect is: the opponent will skip his [drawing]
phase, destroy this artefact at the beginning of the owner
[preparation] phase
*/