using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColourFactory : ColourFactory
{
    public override Colour CreateColour(string name) => new DemoColour(name);
    public override Colour CreateColour(ICollection<string> colors) => new DemoColour(colors);
}