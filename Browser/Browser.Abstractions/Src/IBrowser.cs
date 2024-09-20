using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;

namespace Browser.Abstractions;

public interface IBrowser : INavigationRouter, IBrowserObservable
{
    Task<IPage> CreatePage(IPageCreateOptions? options, CancellationToken token = default);
    
    Task LoadPage(PageId? pageId = default, CancellationToken token = default);
    
    Task ReloadPage(PageId? pageId = default, CancellationToken token = default);
    
    Task RemovePage(PageId pageId);
    
    void SetCurrentPage(PageId pageId);
}
