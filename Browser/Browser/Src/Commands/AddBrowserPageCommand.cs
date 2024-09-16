using Browser.Core;
using Browser.Core.Navigation;
using CommunityToolkit.Mvvm.Messaging;

namespace Browser.Commands;

public class AddBrowserPageCommand : CommandBase<INavigateOptions>
{
    private readonly IBrowser _browser;
    private readonly IMessenger _messenger;

    public AddBrowserPageCommand(IBrowser browser, IMessenger messenger)
    {
        _browser = browser;
        _messenger = messenger;
    }
    
    protected override void OnExecute(INavigateOptions? parameter)
    {
        if (parameter == null)
        {
            parameter = new UrlNavigateOptions("");
        }
        
        _browser.AddPage(new BrowserPage(_messenger, parameter));
    }
}


    