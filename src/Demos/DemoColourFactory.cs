using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColourFactory : ColourFactory
{
    public override Colour CreateColour(string name) => new DemoColour(name);
    public override DualColour CreateDualColour(string firstName, string secondName) => new DemoDualColour(firstName, secondName);
}