using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColourFactory : ColourFactory
{
    public override Red CreateRed(int cost = 0) => new Red(cost);
    public override Blue CreateBlue(int cost = 0) => new Blue(cost);
    public override Colourless CreateColourless(int cost = 0) => new Colourless(cost);
}