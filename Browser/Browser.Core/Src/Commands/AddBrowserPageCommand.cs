using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;

namespace Browser.Core.Commands;

public class AddBrowserPageCommand : CommandBase<INavigateOptions>
{
    private readonly IBrowser _browser;
    private readonly IBrowserPageFactory _browserPageFactory;

    public AddBrowserPageCommand(IBrowser browser, IBrowserPageFactory browserPageFactory)
    {
        _browser = browser;
        _browserPageFactory = browserPageFactory;
    }
    
    protected override void OnExecute(INavigateOptions? parameter)
    {
        if (parameter == null)
        {
            parameter = new UrlNavigateOptions("google.com");
        }
        
        var page = _browserPageFactory.Create(parameter);
        
        _browser.AddPage(page);
    }
}


    