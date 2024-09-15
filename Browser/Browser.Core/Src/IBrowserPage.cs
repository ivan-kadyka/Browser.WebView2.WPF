using Browser.Core.Navigation;

namespace Browser.Core;

public interface IBrowserPage : IBrowserRouter
{
    string Id { get; }
    
    string Title { get; }
}