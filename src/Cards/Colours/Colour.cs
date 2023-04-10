// Jamey Schaap 0950044
// Vincent de Gans 1003196

namespace TheCardGame.Cards.Colours;

public abstract class Colour
{
    public int Cost { get; init; }
    public string Name { get; init; }
    public Colour(string name, int cost = 0)
    {
        Name = name;
        Cost = cost;
    }
}