// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Players.Constraints;

public class SkipDrawing : Constraint
{
    public override string Name { get; init; } = nameof(SkipDrawing);
    public override bool Value { get; init; } = true;
}