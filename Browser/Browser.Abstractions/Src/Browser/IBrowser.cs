using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;

namespace Browser.Abstractions;

public interface IBrowser : IBrowserRouter, IBrowserObservable
{
    Task AddPage(IBrowserPage page);
    
    Task RemovePage(IBrowserPage page);
    
    
    Task ReloadPage(PageId? pageId = default);
    
    
    void SetCurrentPage(IBrowserPage page);
}
