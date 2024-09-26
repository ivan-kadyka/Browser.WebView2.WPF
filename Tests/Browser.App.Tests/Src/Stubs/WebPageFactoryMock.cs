using Browser.Abstractions.Page;
using Browser.Abstractions.Settings;
using Browser.App.Tests.Stubs;
using Browser.Core.UriResolver;
using Browser.Settings.Abstractions;
using Browser.WebPage.Wpf.Factory;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Wpf;

namespace Browser.App.Tests;

internal class WebPageFactoryMock : BrowserPageFactory
{
    public WebPageFactoryMock(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        ILoggerFactory loggerFactory,
        IBrowserPageSettingsProvider pageSettingsProvider, 
        IUriResolver uriResolver) 
        : base(webViewFactory, messenger, loggerFactory, pageSettingsProvider, uriResolver)
    {
    }
    
    protected override IBrowserPage CreatePage(PageId id, IWebView2 webView, IBrowserPageSettings settings, ILogger logger)
    {
        return new WebPageStub(id, _messenger, settings, _uriResolver, logger);
    }
}