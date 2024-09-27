using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Settings;
using Browser.Core.UriResolver;
using Browser.Messenger;
using Browser.WebPage.Wpf.Page;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Wpf;

namespace Browser.WebPage.Tests;

public class WebViewPageTests : IDisposable
{
    private readonly IWebView2 _webView;
    private readonly IMessenger _messenger;
    private readonly WebViewPage _browserPage;

    public WebViewPageTests()
    {
        _webView = Substitute.For<IWebView2>();
        _messenger = Substitute.For<IMessenger>();
        
        var pageId = PageId.New();
        var settings = Substitute.For<IBrowserPageSettings>();
        settings.Source.Returns(new Uri("https://example.com"));
        var uriConverter = Substitute.For<IUriResolver>();
        var logger = Substitute.For<ILogger>();

        _browserPage = new WebViewPage(pageId, _webView, _messenger, settings, uriConverter, logger);
    }

    [Fact]
    public void Forward_CanGoForward_InvokesGoForwardAndSendsMessage()
    {
        // Arrange
        _webView.CanGoForward.Returns(true);

        // Act
        _browserPage.Forward();

        // Assert
        _webView.Received().GoForward();
        _messenger.ReceivedWithAnyArgs().Send(new BrowserForwardMessage());
    }

    [Fact]
    public void Forward_CannotGoForward_DoesNotInvokeGoForward()
    {
        // Arrange
        _webView.CanGoForward.Returns(false);

        // Act
        _browserPage.Forward();

        // Assert
        _webView.DidNotReceive().GoForward();
        _messenger.DidNotReceiveWithAnyArgs().Send(new BrowserForwardMessage());
    }

    [Fact]
    public void Back_CanGoBack_InvokesGoBackAndSendsMessage()
    {
        // Arrange
        _webView.CanGoBack.Returns(true);

        // Act
        _browserPage.Back();

        // Assert
        _webView.Received().GoBack();
        _messenger.ReceivedWithAnyArgs().Send(new BrowserBackMessage());
    }

    [Fact]
    public void Back_CannotGoBack_DoesNotInvokeGoBack()
    {
        // Arrange
        _webView.CanGoBack.Returns(false);

        // Act
        _browserPage.Back();

        // Assert
        _webView.DidNotReceive().GoBack();
        _messenger.DidNotReceiveWithAnyArgs().Send(new BrowserBackMessage());
    }

    [Fact]
    public async Task Load_EnsureCoreWebView2Async_Called()
    {
        // Act
        await _browserPage.Load();

        // Assert
        await _webView.Received().EnsureCoreWebView2Async();
    }

    [Fact]
    public async Task ReloadAsync_Normal_InvokesReloadWebView()
    {
        // Arrange
        var token = new CancellationToken();

        // Act
        await _browserPage.Reload(token);

        // Assert
        _webView.Received().Reload();
    }
    
    [Fact]
    public void Reload_Normal_InvokesReloadWebView()
    {
        // Arrange & Act
        _browserPage.Reload();

        // Assert
        _webView.Received().Reload();
    }

    [Fact]
    public async Task Push_Normal_InvokesNavigate()
    {
        // Arrange
        var options = new NavigateOptions("https://example.com");

        Uri? changedUri = null;
        _browserPage.Source.Subscribe(uri => changedUri = uri);
      
        await _browserPage.Load();

        // Act
        _browserPage.Push(options);

        // Assert
        Assert.NotNull(changedUri);
        Assert.Equal(options.Address, changedUri!.ToString().Trim('/'));
    }

    public void Dispose()
    {
        _browserPage.Dispose();
    }
}