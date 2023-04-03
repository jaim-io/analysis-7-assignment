using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class DemoColour : Colour
{
    public DemoColour(string name)
        : base(name)
    {
    }

    public DemoColour(ICollection<string> names)
        : base(names)
    {
    }
}