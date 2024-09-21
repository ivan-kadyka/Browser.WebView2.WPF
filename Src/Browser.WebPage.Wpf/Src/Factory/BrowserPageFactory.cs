using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Core.Pages;
using Browser.Settings.Abstractions;
using Browser.WebPage.Wpf.Page;
using Browser.WebPage.Wpf.Utils;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;

namespace Browser.WebPage.Wpf.Factory;

internal class BrowserPageFactory : IBrowserPageFactory
{
    private readonly IWebViewFactory _webViewFactory;
    private readonly IMessenger _messenger;
    private readonly IBrowserPageSettingsProvider _pageSettingsProvider;
    private readonly IBrowserSettings _browserSettings;
    private readonly ILoggerFactory _loggerFactory;

    public BrowserPageFactory(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        ILoggerFactory loggerFactory, 
        IBrowserPageSettingsProvider pageSettingsProvider,
        IBrowserSettings browserSettings)
    {
        _webViewFactory = webViewFactory;
        _messenger = messenger;
        _loggerFactory = loggerFactory;
        _pageSettingsProvider = pageSettingsProvider;
        _browserSettings = browserSettings;
    }
    
    public IBrowserPage Create(IPageCreateOptions options)
    {
        var id = PageId.New();
        
        var logger = _loggerFactory.CreateLogger($"WebPage[{id}]");
        var settings = _pageSettingsProvider.Get(options);
        var uriConverter = new UriConverter(_browserSettings);
        
        var webView =  _webViewFactory.Create(options);
        var webViewPage = new WebViewPage(id, webView, _messenger, settings,uriConverter, logger);
        
        var page = new ExceptionDecoratorPage(webViewPage, logger);
        return page;
    }
}