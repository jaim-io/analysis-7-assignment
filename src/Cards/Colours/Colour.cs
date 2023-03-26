namespace TheCardGame.Cards.Colours;

public abstract class Colour
{
    public string Name { get; init; }
    public Colour(string name)
    {
        Name = name;
    }
}