using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

public interface IBrowserPage : IBrowserRouter
{
    PageId Id { get; }
    
    string Title { get; }
    
    object Content { get; }
    
    Task Load(CancellationToken token = default);
    
    Task Reload(CancellationToken token = default);
}