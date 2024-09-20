namespace Browser.Abstractions.Exceptions;

public enum PageError
{
    Unknown,
    Load,
    Reload,
    Forward,
    Back,
}


public class BrowserPageException : BrowserBaseException
{
    public PageError Error { get; }
    
    
    public BrowserPageException(PageError error, string message, Exception? innerException = default) 
        : base(message, innerException)
    {
        Error = error;
    }
}