namespace TheCardGame.Cards.Colours;

public abstract class Colour
{
    public List<string> Names { get; init; } = new();
    public Colour(string name)
    {
        Names.Append(name);
    }

    public Colour(ICollection<string> names)
    {
        Names = names.ToList();
    }
}