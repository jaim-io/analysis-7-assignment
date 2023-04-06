using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColourFactory : ColourFactory
{
    public override Colour CreateRed(int cost = 0) => new Red(cost);
    public override Colour CreateBlue(int cost = 0) => new Blue(cost);
    public override Colour CreateColourless(int cost = 0) => new Colourless(cost);
}