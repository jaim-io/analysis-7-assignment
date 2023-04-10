// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Effects.States;

public class Used : EffectState
{
    public Used(Effect effect)
        : base(effect)
    {
    }

    public Used(EffectState state)
        : base(state.effect)
    {
    }
}