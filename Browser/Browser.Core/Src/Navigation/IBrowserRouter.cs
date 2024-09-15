namespace Browser.Core.Navigation;

public interface IBrowserRouter : IBrowserPathRouter
{
    void Forward();
    
    bool CanForward { get; }


    void Back();
    
    
    bool CanBack { get; }
    
    
    void Refresh();
    
    
    void Push(INavigateOptions options);
    
    
    void Replace(INavigateOptions options);
}

public static class BrowserRouterExtensions
{
    public static void Navigate(this IBrowserRouter router, string address)
    {
        router.Push(new UrlNavigateOptions(address));
    }
}