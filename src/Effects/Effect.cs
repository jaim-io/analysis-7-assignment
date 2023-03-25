namespace TheCardGame.Effects;

public abstract class Effect {
    // State?
    // Duration?
    // Dispose
    public Action Value;

    public Effect(Action value)
    {
        Value = value;
    }

    public abstract void Activate();
}

// Card <>-- Effects
//     1   0..*

// Is activated when succesfully played

// Effects can influence players, other cards in play, or the hands of a player. 
// Effects can influence phases of the game and can modify so the gameplay.

// The effects that require a target cannot be activated if the target is not 
//      legitimate (e.g.: if a card affects a land but no lands are present, the effect
//      will not occur).

// Instantaneous cards are considered cards that play automatically the
// effect(s) and, after that, are discarded.


// >>>> Card.cs <<<<
// public List<Effect> ManuallyTriggerdEffects { get; init; }
// public List<Effect> OnPlayEffects { get; init; }

// singleton -> GameBoard / Stack
// observer -> TemporaryEffect - ????
// factory -> EffectFactory
// state -> ?EffectState?
