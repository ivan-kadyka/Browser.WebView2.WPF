namespace Browser.Abstractions.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a page-related error occurs in the browser.
/// </summary>
public class BrowserPageException : BrowserBaseException
{
    /// <summary>
    /// Gets the type of page error that caused the exception.
    /// </summary>
    public PageError Error { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserPageException"/> class with the specified error, message, and optional inner exception.
    /// </summary>
    /// <param name="error">The page error that occurred.</param>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">Optional inner exception for detailed error tracing.</param>
    public BrowserPageException(PageError error, string message, Exception? innerException = default) 
        : base(message, innerException)
    {
        Error = error;
    }
}
