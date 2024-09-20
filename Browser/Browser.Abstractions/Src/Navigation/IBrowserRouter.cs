namespace Browser.Abstractions.Navigation;

public interface IBrowserRouter : IPathObservable
{
    void Forward();
    
    bool CanForward { get; }


    void Back();
    
    
    bool CanBack { get; }
    
    
    void Refresh();
    
    bool CanRefresh { get; }
    
    
    void Push(INavigateOptions options);
}

public static class BrowserRouterExtensions
{
    public static void Navigate(this IBrowserRouter router, string address)
    {
        router.Push(new UrlNavigateOptions(address));
    }
}