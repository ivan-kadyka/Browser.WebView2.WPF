namespace Browser.Abstractions.Exceptions;

public abstract class BrowserBaseException : Exception
{
    protected BrowserBaseException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}