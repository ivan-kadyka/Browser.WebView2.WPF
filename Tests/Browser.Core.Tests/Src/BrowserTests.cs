using Browser.Abstractions;
using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Settings.Abstractions;
using Browser.Settings.Sections;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;

namespace Browser.Core.Tests;

public class BrowserTests : IDisposable
{
    private readonly IMessenger _messenger;
    private readonly IBrowserPageFactory _browserPageFactory;
    private readonly IBrowserSettings _browserSettings;
    private readonly ILogger<IBrowser> _logger;
    private readonly IBrowser _browser;

    public BrowserTests()
    {
        _messenger = Substitute.For<IMessenger>();
        _browserPageFactory = Substitute.For<IBrowserPageFactory>();
        _browserSettings = Substitute.For<IBrowserSettings>();

        var generalSettings = new GeneralSettings { HomeAddress = "about:blank" };
        _browserSettings.General.Returns(generalSettings);
        
        _logger = LoggerFactory.Create(builder => {}).CreateLogger<IBrowser>();
        
        _browserPageFactory.Create(Arg.Any<IPageCreateOptions>()).Returns(it =>
        {
            var options = it.Arg<IPageCreateOptions>();
            
            var page = Substitute.For<IBrowserPage>();
            
            page.Id.Returns(PageId.New());
            page.Source.Value.Returns(options.Source);
            
            return page;
        });
        
        _browser = new Browser(_messenger, _browserPageFactory, _browserSettings, _logger);
    }
    
    

    [Fact]
    public async Task LoadPage_WithPageId_LoadsThePage()
    {
        // Arrange
        var pageId = new PageId("1");
        var page = Substitute.For<IBrowserPage>();
        page.Id.Returns(pageId);
        _browserPageFactory.Create(Arg.Any<IPageCreateOptions>()).Returns(page);
        await _browser.CreatePage(null);

        // Act
        await _browser.LoadPage(pageId);

        // Assert
        await page.Received().Load(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RemovePage_WithPageId_RemovesThePage()
    {
        // Arrange
        var pageId = new PageId("1");
        var page = Substitute.For<IBrowserPage>();
        page.Id.Returns(pageId);
        _browserPageFactory.Create(Arg.Any<IPageCreateOptions>()).Returns(page);
        await _browser.CreatePage(null);

        // Act
        await _browser.RemovePage(pageId);

        // Assert
        Assert.DoesNotContain(page, _browser.Pages);
        page.Received().Dispose();
    }

    [Fact]
    public async Task ReloadPage_WithPageId_ReloadsThePage()
    {
        // Arrange
        var pageId = new PageId("1");
        var page = Substitute.For<IBrowserPage>();
        page.Id.Returns(pageId);
        _browserPageFactory.Create(Arg.Any<IPageCreateOptions>()).Returns(page);
        await _browser.CreatePage(null);

        // Act
        await _browser.ReloadPage(pageId);

        // Assert
        await page.Received().Reload(Arg.Any<CancellationToken>());
    }

    [Fact]
    public void SetCurrentPage_WithValidPageId_SetsActivePage()
    {
        // Arrange
        var page = Substitute.For<IBrowserPage>();
        page.Id.Returns(new PageId("1"));
        _browserPageFactory.Create(Arg.Any<IPageCreateOptions>()).Returns(page);
        _browser.CreatePage(null).Wait();

        // Act
        _browser.SetCurrentPage(page.Id);

        // Assert
        Assert.Equal(page, _browser.CurrentPage.Value);
    }

    public void Dispose()
    {
        _browser.Dispose();
    }
}

