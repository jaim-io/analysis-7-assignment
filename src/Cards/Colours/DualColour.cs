namespace TheCardGame.Cards.Colours;

public class DualColour : Colour
{
    public string SecondName { get; init; }
    public DualColour(string firstName, string secondName)
        : base(firstName)
    {
        this.SecondName = secondName;
    }
}