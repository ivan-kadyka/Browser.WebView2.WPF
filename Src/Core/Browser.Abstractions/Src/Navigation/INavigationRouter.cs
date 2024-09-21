namespace Browser.Abstractions.Navigation;

/// <summary>
/// Defines the contract for navigation routing, providing methods to navigate forward, backward, reload, and push new navigation options. 
/// It also provides properties indicating whether forward, back, or reload actions are possible.
/// </summary>
public interface INavigationRouter : IPathObservable
{
    /// <summary>
    /// Navigates forward in the navigation history.
    /// </summary>
    void Forward();
    
    /// <summary>
    /// Gets a value indicating whether navigation forward is possible.
    /// </summary>
    bool CanForward { get; }

    /// <summary>
    /// Navigates backward in the navigation history.
    /// </summary>
    void Back();
    
    /// <summary>
    /// Gets a value indicating whether navigation backward is possible.
    /// </summary>
    bool CanBack { get; }
    
    /// <summary>
    /// Reloads the current navigation context.
    /// </summary>
    void Reload();
    
    /// <summary>
    /// Gets a value indicating whether the reload action is possible.
    /// </summary>
    bool CanReload { get; }

    /// <summary>
    /// Pushes new navigation options to change the current navigation context.
    /// </summary>
    /// <param name="options">Navigation options containing the address or path to navigate to.</param>
    void Push(INavigateOptions options);
}


public static class NavigationRouterExtensions
{
    public static void Navigate(this INavigationRouter router, string address)
    {
        router.Push(new NavigateOptions(address));
    }
}