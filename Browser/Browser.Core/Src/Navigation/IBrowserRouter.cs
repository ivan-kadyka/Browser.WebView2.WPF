namespace Browser.Core.Navigation;

public interface IBrowserRouter : IPathRouter
{
    void Forward();


    void Back();
    
    
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