using Browser.Abstractions.Page;
using Browser.Page.Wpf.Page;
using CommunityToolkit.Mvvm.Messaging;

namespace Browser.Page.Wpf.Factory;

internal class BrowserPageFactory : IBrowserPageFactory
{
    private readonly IWebViewFactory _webViewFactory;
    private readonly IMessenger _messenger;
    private readonly IBrowserSettings _browserSettings;

    public BrowserPageFactory(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        IBrowserSettings browserSettings)
    {
        _webViewFactory = webViewFactory;
        _messenger = messenger;
        _browserSettings = browserSettings;
    }
    
    public IBrowserPage Create(IPageCreateOptions options)
    {
        var settings = _browserSettings.CreatePage(options);
        var webView =  _webViewFactory.Create(options);
        var id = PageId.New();
        
        var page = new BrowserPage(id, webView, _messenger, settings);
        
        return page;
    }
}