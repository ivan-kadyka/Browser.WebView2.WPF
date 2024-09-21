namespace Browser.Abstractions.Page.Factory;

/// <summary>
/// Defines a factory interface for creating browser pages based on page creation options.
/// </summary>
public interface IBrowserPageFactory
{
    /// <summary>
    /// Creates a new browser page using the specified creation options.
    /// </summary>
    /// <param name="options">Options for creating the page.</param>
    /// <returns>A newly created browser page.</returns>
    IBrowserPage Create(IPageCreateOptions options);
}