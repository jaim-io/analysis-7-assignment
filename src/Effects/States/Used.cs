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