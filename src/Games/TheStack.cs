using TheCardGame.Effects;

namespace TheCardGame.Games;

public class TheStack
{
    private List<Effect> _stack;
    private int _resolveCounter;
    public int Count { get => this._stack.Count; }
    public TheStack() => _stack = new();

    public TheStack(ICollection<Effect> stack) => _stack = new(stack);

    public Effect? Peek() => _stack.Count > 0 ? _stack[_stack.Count - 1] : null;
    public Effect? Pop()
    {
        var effect = this.Peek();
        _stack.RemoveAt(_stack.Count - 1);
        return effect;
    }
    public void Push(Effect effect) => _stack.Add(effect);
    public void Clear() => _stack.RemoveAll(_ => true);

    public void Skip(uint offset) => this._resolveCounter -= (int)offset;

    public void Resolve()
    {
        this._resolveCounter = _stack.Count - 1;
        for (int i = _resolveCounter; i >= 0; i--)
        {
            _stack[i].Trigger();
        }
        this.Clear();
    }
}