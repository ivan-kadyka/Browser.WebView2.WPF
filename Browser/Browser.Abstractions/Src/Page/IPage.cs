using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

public interface IPage : IPathObservable
{
    PageId Id { get; }
    
    string Title { get; }
    
    object Content { get; }
}