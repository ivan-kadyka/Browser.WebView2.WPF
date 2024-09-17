using Browser.Abstractions.Content;
using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

public interface IBrowserPage : IBrowserRouter
{
    string Id { get; }
    
    string Title { get; }
    
    object Content { get; }
    
    Task Load(CancellationToken token = default);
}