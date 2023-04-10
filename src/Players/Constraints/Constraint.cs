// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Players.Constraints;

public abstract class Constraint
{
    public abstract string Name { get; init; }
    public abstract bool Value { get; init; }
}