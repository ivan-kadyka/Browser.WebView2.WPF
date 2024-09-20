using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;

namespace Browser.Abstractions;

public interface IBrowser : IBrowserRouter, IBrowserObservable
{
    Task<IPage> CreatePage(IPageCreateOptions? options, CancellationToken token = default);
    
    Task RemovePage(PageId pageId);
    
    Task LoadPage(PageId? pageId = default, CancellationToken token = default);
    
    Task ReloadPage(PageId? pageId = default, CancellationToken token = default);
    
    void SetCurrentPage(PageId pageId);
}
