using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Page.Wpf.Page;
using CommunityToolkit.Mvvm.Messaging;

namespace Browser.Page.Wpf.Factory;

internal class BrowserPageFactory : IBrowserPageFactory
{
    private readonly IWebViewFactory _webViewFactory;
    private readonly IMessenger _messenger;

    public BrowserPageFactory(IWebViewFactory webViewFactory, IMessenger messenger)
    {
        _webViewFactory = webViewFactory;
        _messenger = messenger;
    }
    
    public IBrowserPage Create(INavigateOptions options)
    {
        var webView =  _webViewFactory.Create();
        var id = PageId.New();
        
        var page = new BrowserPage(id, webView, _messenger, options);
        
        return page;
    }
}