using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

public interface IBrowserPage : IPage, IBrowserRouter, IDisposable
{
    Task Load(CancellationToken token = default);
    
    Task Reload(CancellationToken token = default);
}