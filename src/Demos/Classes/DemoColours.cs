using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class Red : Colour
{
    public Red(int cost)
        : base(nameof(Red), cost)
    {
    }
}

public class Blue : Colour
{
    public Blue(int cost)
        : base(nameof(Blue), cost)
    {
    }
}

public class Colourless : Colour
{
    public Colourless(int cost)
        : base(nameof(Colourless), cost)
    {
    }
}