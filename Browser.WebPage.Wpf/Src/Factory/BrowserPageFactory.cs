using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Core.Pages;
using Browser.Settings.Abstractions;
using Browser.WebPage.Wpf.Page;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;

namespace Browser.WebPage.Wpf.Factory;

internal class BrowserPageFactory : IBrowserPageFactory
{
    private readonly IWebViewFactory _webViewFactory;
    private readonly IMessenger _messenger;
    private readonly IBrowserPageSettingsProvider _pageSettingsProvider;
    private readonly ILoggerFactory _loggerFactory;

    public BrowserPageFactory(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        ILoggerFactory loggerFactory, 
        IBrowserPageSettingsProvider pageSettingsProvider)
    {
        _webViewFactory = webViewFactory;
        _messenger = messenger;
        _loggerFactory = loggerFactory;
        _pageSettingsProvider = pageSettingsProvider;
    }
    
    public IBrowserPage Create(IPageCreateOptions options)
    {
        var id = PageId.New();
        
        var logger = _loggerFactory.CreateLogger($"WebPage[{id}]");
        var settings = _pageSettingsProvider.Get(options);
        
        var webView =  _webViewFactory.Create(options);
        var webViewPage = new WebViewPage(id, webView, _messenger, settings, logger);
        
        var page = new ExceptionDecoratorPage(webViewPage, logger);
        return page;
    }
}