// Jamey Schaap 0950044
// Vincent de Gans 1003196

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