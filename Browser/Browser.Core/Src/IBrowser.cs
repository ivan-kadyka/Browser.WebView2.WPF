using Browser.Core.Navigation;

namespace Browser.Core;

public interface IBrowser : IBrowserRouter, IBrowserObservable
{
    Task AddPage(IBrowserPage page);
    
    Task RemovePage(IBrowserPage page);
    
    
    void SetCurrentPage(IBrowserPage page);
}
