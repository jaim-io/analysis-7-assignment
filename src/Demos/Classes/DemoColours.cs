using TheCardGame.Cards.Colours;

namespace TheCardGame.Demos;

public class Red : Colour
{
    public Red(int cost)
        : base(nameof(Red))
    {
    }
}

public class Blue : Colour
{
    public Blue(int cost)
        : base(nameof(Blue))
    {
    }
}

public class Colourless : Colour
{
    public Colourless(int cost)
        : base(nameof(Colourless))
    {
    }
}