using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColourFactory : ColourFactory
{
    public override Colour CreateRed() => new Red();
    public override Colour CreateBlue() => new Blue();
}