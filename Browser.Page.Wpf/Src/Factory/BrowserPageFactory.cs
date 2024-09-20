using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;
using Browser.Core.Pages;
using Browser.Page.Wpf.Page;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;

namespace Browser.Page.Wpf.Factory;

internal class BrowserPageFactory : IBrowserPageFactory
{
    private readonly IWebViewFactory _webViewFactory;
    private readonly IMessenger _messenger;
    private readonly IBrowserSettings _browserSettings;
    private readonly ILoggerFactory _loggerFactory;

    public BrowserPageFactory(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        IBrowserSettings browserSettings,
        ILoggerFactory loggerFactory)
    {
        _webViewFactory = webViewFactory;
        _messenger = messenger;
        _browserSettings = browserSettings;
        _loggerFactory = loggerFactory;
    }
    
    public IBrowserPage Create(IPageCreateOptions options)
    {
        var id = PageId.New();
        
        var logger = _loggerFactory.CreateLogger($"WebPage[{id}]");
        var settings = _browserSettings.CreatePage(options);
        
        var webView =  _webViewFactory.Create(options);
        var webViewPage = new WebViewPage(id, webView, _messenger, settings, logger);
        
        var page = new ExceptionDecoratorPage(webViewPage, logger);
        return page;
    }
}