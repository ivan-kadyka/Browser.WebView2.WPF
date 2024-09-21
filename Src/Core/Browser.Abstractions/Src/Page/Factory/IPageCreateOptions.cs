namespace Browser.Abstractions.Page.Factory;

/// <summary>
/// Defines the options for creating a page, including the source URI and whether the page should be set as active.
/// </summary>
public interface IPageCreateOptions
{
    /// <summary>
    /// Gets the source URI for the page to be created.
    /// </summary>
    Uri Source { get; }

    /// <summary>
    /// Gets a value indicating whether the created page should be set as the active page.
    /// </summary>
    bool SetActive { get; }
}