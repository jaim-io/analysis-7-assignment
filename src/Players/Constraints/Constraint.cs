namespace TheCardGame.Players.Constraints;

public abstract class Constraint
{
    public abstract string Name { get; init; }
    public abstract bool Value { get; init; }
}