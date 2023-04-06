namespace TheCardGame.Cards.Colours;

public abstract class ColourFactory
{
    public abstract Colour CreateRed(int cost = 0);
    public abstract Colour CreateBlue(int cost = 0);
    public abstract Colour CreateColourless(int cost = 0);
}