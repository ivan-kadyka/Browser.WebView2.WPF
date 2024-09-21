namespace Browser.Abstractions.Exceptions;

/// <summary>
/// Enumerates the possible errors that can occur during page navigation or loading.
/// </summary>
public enum PageError
{
    /// <summary>
    /// Represents an unknown error.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Represents an error that occurred while loading the page.
    /// </summary>
    Load,
    
    /// <summary>
    /// Represents an error that occurred while reloading the page.
    /// </summary>
    Reload,
    
    /// <summary>
    /// Represents an error that occurred while navigating forward.
    /// </summary>
    Forward,
    
    /// <summary>
    /// Represents an error that occurred while navigating back.
    /// </summary>
    Back,
}