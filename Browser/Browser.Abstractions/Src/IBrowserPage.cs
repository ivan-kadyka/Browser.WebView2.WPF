using Browser.Abstractions.Content;
using Browser.Abstractions.Navigation;

namespace Browser.Abstractions;

public interface IBrowserPage : IBrowserRouter
{
    string Id { get; }
    
    string Title { get; }
    
    IContent Content { get; }
}