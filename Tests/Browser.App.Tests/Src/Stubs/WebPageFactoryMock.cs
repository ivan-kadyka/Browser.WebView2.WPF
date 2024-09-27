using Browser.Abstractions.Page;
using Browser.Abstractions.Settings;
using Browser.App.Tests.Stubs;
using Browser.App.Tests.Utils;
using Browser.Core.UriResolver;
using Browser.Settings.Abstractions;
using Browser.WebPage.Wpf.Factory;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Wpf;

namespace Browser.App.Tests;

internal class WebPageFactoryMock : BrowserPageFactory
{
    private readonly TestExceptionProxy _testExceptionProxy;

    public WebPageFactoryMock(
        IWebViewFactory webViewFactory,
        IMessenger messenger,
        ILoggerFactory loggerFactory,
        IBrowserPageSettingsProvider pageSettingsProvider, 
        IUriResolver uriResolver,
        TestExceptionProxy testExceptionProxy) 
        : base(webViewFactory, messenger, loggerFactory, pageSettingsProvider, uriResolver)
    {
        _testExceptionProxy = testExceptionProxy;
    }
    
    protected override IBrowserPage CreatePage(PageId id, IWebView2 webView, IBrowserPageSettings settings, ILogger logger)
    {
        return new WebPageStub(id, _messenger, settings, _uriResolver, _testExceptionProxy, logger);
    }
}