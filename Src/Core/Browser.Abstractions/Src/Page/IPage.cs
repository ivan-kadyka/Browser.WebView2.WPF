using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

/// <summary>
/// Represents a page within the browser, containing an identifier, title, and content. 
/// It is also an observable path that provides access to the source URI.
/// </summary>
public interface IPage : IPathObservable
{
    /// <summary>
    /// Gets the unique identifier for the page.
    /// </summary>
    PageId Id { get; }

    /// <summary>
    /// Gets the title of the page.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the content of the page.
    /// </summary>
    object Content { get; }
}