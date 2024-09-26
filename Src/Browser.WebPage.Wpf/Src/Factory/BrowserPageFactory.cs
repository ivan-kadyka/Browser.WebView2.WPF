using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;
using Browser.Core.Pages;
using Browser.Core.UriResolver;
using Browser.Settings.Abstractions;
using Browser.WebPage.Wpf.Page;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Wpf;

namespace Browser.WebPage.Wpf.Factory;

internal class BrowserPageFactory : IBrowserPageFactory
{
    protected readonly IWebViewFactory _webViewFactory;
    protected readonly IMessenger _messenger;
    protected readonly IBrowserPageSettingsProvider _pageSettingsProvider;
    protected readonly IUriResolver _uriResolver;
    protected readonly ILoggerFactory _loggerFactory;

    public BrowserPageFactory(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        ILoggerFactory loggerFactory, 
        IBrowserPageSettingsProvider pageSettingsProvider,
        IUriResolver uriResolver)
    {
        _webViewFactory = webViewFactory;
        _messenger = messenger;
        _loggerFactory = loggerFactory;
        _pageSettingsProvider = pageSettingsProvider;
        _uriResolver = uriResolver;
    }
    
    public IBrowserPage Create(IPageCreateOptions options)
    {
        var id = PageId.New();
        
        var logger = _loggerFactory.CreateLogger($"WebPage[{id}]");
        var settings = _pageSettingsProvider.Get(options);
        
        var webView =  _webViewFactory.Create(options);
        var webViewPage = CreatePage(id, webView, settings, logger);
        
        var page = new ExceptionDecoratorPage(webViewPage, logger);
        return page;
    }
    
    protected virtual IBrowserPage CreatePage(PageId id, IWebView2 webView, IBrowserPageSettings settings, ILogger logger)
    {
        return new WebViewPage(id, webView, _messenger, settings, _uriResolver, logger);
    }
}