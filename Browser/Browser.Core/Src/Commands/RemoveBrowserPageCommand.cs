using Browser.Abstractions;
using Browser.Abstractions.Page;

namespace Browser.Core.Commands;

public class RemoveBrowserPageCommand : CommandBase<PageId>
{
    private readonly IBrowser _browser;

    public RemoveBrowserPageCommand(IBrowser browser)
    {
        _browser = browser;
    }
    
    protected override void OnExecute(PageId? pageId)
    {
        if (pageId == null)
        {
            pageId = _browser.CurrentPage.Value.Id;
        }
        
        var page = _browser.Pages.FirstOrDefault(it => it.Id == pageId);
        
        if (page != null)
            _browser.RemovePage(page);
    }
}