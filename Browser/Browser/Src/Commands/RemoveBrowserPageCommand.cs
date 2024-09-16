using Browser.Core;

namespace Browser.Commands;

public class RemoveBrowserPageCommand : CommandBase<string>
{
    private readonly IBrowser _browser;

    public RemoveBrowserPageCommand(IBrowser browser)
    {
        _browser = browser;
    }
    
    protected override void OnExecute(string? pageId)
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