using TheCardGame.Effects;
using TheCardGame.Effects.States;

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
    public void Clear()
    {
        _stack.ForEach(e =>
        {
            if (e.State is OnTheStack)
            {
                e.Dispose();
            }
        });
        _stack.RemoveAll(_ => true);
    }

    public void Skip(uint offset) => this._resolveCounter -= (int)offset;

    public void Resolve()
    {
        this._resolveCounter = _stack.Count - 1;
        while (_resolveCounter >= 0)
        {
            Console.WriteLine($"[Stack] Resolving effect: {_stack[_resolveCounter].Name}");
            _stack[_resolveCounter].Trigger();
            _resolveCounter--;
        }

        this.Clear();
    }
}