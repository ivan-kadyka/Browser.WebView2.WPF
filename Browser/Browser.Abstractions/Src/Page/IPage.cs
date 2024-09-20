using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

public interface IPage : IBrowserPathRouter
{
    PageId Id { get; }
    
    string Title { get; }
    
    object Content { get; }
}