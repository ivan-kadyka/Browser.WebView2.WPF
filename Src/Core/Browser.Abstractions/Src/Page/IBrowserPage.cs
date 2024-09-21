using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

/// <summary>
/// Represents a browser page that supports navigation actions and is disposable. 
/// It provides methods to load and reload the page, and manages navigation history.
/// </summary>
public interface IBrowserPage : IPage, INavigationRouter, IDisposable
{
    /// <summary>
    /// Loads the page asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token to cancel the load operation.</param>
    /// <returns>A task representing the asynchronous load operation.</returns>
    Task Load(CancellationToken token = default);

    /// <summary>
    /// Reloads the page asynchronously.
    /// </summary>
    /// <param name="token">A cancellation token to cancel the reload operation.</param>
    /// <returns>A task representing the asynchronous reload operation.</returns>
    Task Reload(CancellationToken token);
}