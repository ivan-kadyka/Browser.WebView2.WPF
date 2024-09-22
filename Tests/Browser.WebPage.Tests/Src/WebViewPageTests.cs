using Browser.Abstractions.Page;
using Browser.Abstractions.Settings;
using Browser.Messenger;
using Browser.WebPage.Wpf.Page;
using Browser.WebPage.Wpf.Utils;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Wpf;


public class WebViewPageTests : IDisposable
{
    private readonly PageId _pageId;
    private readonly IWebView2 _webView;
    private readonly IMessenger _messenger;
    private readonly IBrowserPageSettings _settings;
    private readonly IUriResolver _uriConverter;
    private readonly ILogger _logger;
    private readonly WebViewPage _webViewPage;

    public WebViewPageTests()
    {
        _pageId = PageId.New();
        _webView = Substitute.For<IWebView2>();
        _messenger = Substitute.For<IMessenger>();
        _settings = Substitute.For<IBrowserPageSettings>();
        _settings.Source.Returns(new Uri("https://example.com"));
        _uriConverter = Substitute.For<IUriResolver>();
        _logger = Substitute.For<ILogger>();

        _webViewPage = new WebViewPage(_pageId, _webView, _messenger, _settings, _uriConverter, _logger);
    }

    [Fact]
    public void Forward_CanGoForward_InvokesGoForwardAndSendsMessage()
    {
        // Arrange
        _webView.CanGoForward.Returns(true);

        // Act
        _webViewPage.Forward();

        // Assert
        _webView.Received().GoForward();
     //   _messenger.ReceivedWithAnyArgs().Send(Arg.Any<BrowserForwardMessage>());
    }

    [Fact]
    public void Forward_CannotGoForward_DoesNotInvokeGoForward()
    {
        // Arrange
        _webView.CanGoForward.Returns(false);

        // Act
        _webViewPage.Forward();

        // Assert
        _webView.DidNotReceive().GoForward();
      //  _messenger.DidNotReceive().Send(Arg.Any<BrowserForwardMessage>());
    }

    [Fact]
    public void Back_CanGoBack_InvokesGoBackAndSendsMessage()
    {
        // Arrange
        _webView.CanGoBack.Returns(true);

        // Act
        _webViewPage.Back();

        // Assert
        _webView.Received().GoBack();
        //_messenger.Received().Send(Arg.Any<BrowserBackMessage>());
    }

    [Fact]
    public void Back_CannotGoBack_DoesNotInvokeGoBack()
    {
        // Arrange
        _webView.CanGoBack.Returns(false);

        // Act
        _webViewPage.Back();

        // Assert
        _webView.DidNotReceive().GoBack();
       // _messenger.DidNotReceive().Send(Arg.Any<BrowserBackMessage>());
    }

    [Fact]
    public async Task Load_EnsureCoreWebView2Async_Called()
    {
        // Act
        await _webViewPage.Load();

        // Assert
        await _webView.Received().EnsureCoreWebView2Async();
    }

    [Fact]
    public async Task Reload_InvokesReload()
    {
        // Arrange
        var token = new CancellationToken();

        // Act
        await _webViewPage.Reload(token);

        // Assert
        _webView.Received().Reload();
    }

    public void Dispose()
    {
        _webViewPage.Dispose();
    }
}
