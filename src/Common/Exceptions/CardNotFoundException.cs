namespace TheCardGame.Utils.Exceptions;

public class CardNotFoundException
    : System.Exception
{
    public CardNotFoundException()
    {
    }

    public CardNotFoundException(string message)
        : base(message)
    {
    }

    public CardNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}