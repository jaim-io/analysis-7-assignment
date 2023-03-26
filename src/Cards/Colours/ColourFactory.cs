namespace TheCardGame.Cards.Colours;

public abstract class ColourFactory
{
    public abstract Colour CreateColour(string name);
    public abstract DualColour CreateDualColour(string firstName, string secondName);
}