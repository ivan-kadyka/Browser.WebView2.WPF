using Browser.Abstractions;
using Browser.Abstractions.Page;

namespace Browser.Core.Commands;

public class ReloadBrowserPageCommand : CommandBase<PageId>
{
    private readonly IBrowser _browser;

    public ReloadBrowserPageCommand(IBrowser browser)
    {
        _browser = browser;
    }
    
    protected override async void OnExecute(PageId? pageId)
    {
       await _browser.ReloadPage(pageId);
    }
}