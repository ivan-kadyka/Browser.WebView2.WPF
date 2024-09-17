using Browser.Abstractions.Navigation;

namespace Browser.Abstractions;

public interface IBrowser : IBrowserRouter, IBrowserObservable
{
    Task AddPage(IBrowserPage page);
    
    Task RemovePage(IBrowserPage page);
    
    
    void SetCurrentPage(IBrowserPage page);
}
