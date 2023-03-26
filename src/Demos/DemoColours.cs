using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColour : Colour
{
    public DemoColour(string name)
        : base(name)
    {
    }
}

public class DemoDualColour : DualColour
{
    public DemoDualColour(string firstName, string secondName)
        : base(firstName, secondName)
    {
    }
}