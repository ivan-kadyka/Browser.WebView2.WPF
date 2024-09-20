using Browser.Abstractions;
using Browser.Abstractions.Page;

namespace Browser.Core.Commands;

public class SelectBrowserPageCommand : CommandBase<PageId>
{
    private readonly IBrowser _browser;

    public SelectBrowserPageCommand(IBrowser browser)
    {
        _browser = browser;
    }
    
    protected override void OnExecute(PageId? pageId)
    {
        if (pageId == null)
            return;
        
        var page = _browser.Pages.FirstOrDefault(it => it.Id == pageId);
        
        if (page != null)
            _browser.SetCurrentPage(page.Id);
    }
}