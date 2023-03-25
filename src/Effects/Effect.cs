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
