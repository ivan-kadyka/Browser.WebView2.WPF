using System.Reactive.Disposables;
using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.App.Tests.Stubs;
using Disposable;

namespace Browser.App.Tests;

public class BrowserAppTests : DisposableBase, IClassFixture<AppServiceFixture>
{
    private readonly AppServiceFixture _appService;
    private readonly CompositeDisposable _disposables = new();

    public BrowserAppTests(AppServiceFixture appService)
    {
        _appService = appService;
    }
    
    
    [Theory]
    [InlineData("https://example.com")]
    public void Navigate_SetAddress_CurrentPageShouldBeHasTheSameAddress(string newAddress)
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var newUri = new Uri(newAddress);
        Uri raisedUri = null;
        
        _disposables.Add(browser.Source.Subscribe(it =>
        {
            raisedUri = it;
        }));

        // Act
        browser.Navigate(newAddress);
        
        // Assert
        var currentPageUri = browser.Source.Value;
        Assert.Equal(currentPageUri, newUri);
        
        Assert.NotNull(raisedUri);
        Assert.Equal(raisedUri, newUri);
    }
    
    
    [Theory]
    [InlineData("https://example.com")]
    public void Navigate_Back_CurrentPageShouldBeHasTheOriginalAddress(string newAddress)
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var expectedUri = browser.CurrentPage.Value.Source.Value;

        // Act
        browser.Navigate(newAddress);
        var canBack = browser.CanBack;
        browser.Back();
        
        // Assert
        Assert.True(canBack);
        Assert.Equal(expectedUri, browser.Source.Value);
    }
    
    [Theory]
    [InlineData("https://example.com")]
    public void Navigate_BackForward_CurrentPageShouldBeHasTheNewAddress(string newAddress)
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();

        // Act
        browser.Navigate(newAddress);
        browser.Back();
        browser.Forward();
        
        // Assert
        Assert.Equal(newAddress, browser.Source.Value.ToString().TrimEnd('/'));
    }
    
    [Fact]
    public async Task Reload_CurrentPage_ShouldBeSuccess()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var currentPage = browser.CurrentPage.Value;
        IPage reloadedPage = null;
        
        _disposables.Add(browser.PageReloaded.Subscribe(it =>
        {
            reloadedPage = it;
        }));

        // Act
        await browser.ReloadPage();
        
        // Assert
        Assert.NotNull(reloadedPage);
        Assert.Same(browser.CurrentPage.Value, reloadedPage);
        Assert.Same(currentPage, browser.CurrentPage.Value);
    }
    
    [Fact]
    public async Task CreatePage_NewTab_ShouldBeSuccess()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var currentPage = browser.CurrentPage.Value;
        var pagesCount = browser.Pages.Count;
        IPage newPage = null;
        
        _disposables.Add(browser.PageAdded.Subscribe(it =>
        {
            newPage = it;
        }));

        // Act
        var resultPage = await browser.CreatePage(new PageCreateOptions(new Uri("https://example.com")));
        
        // Assert
        Assert.NotNull(newPage);
        Assert.Same(browser.CurrentPage.Value, newPage);
        Assert.NotSame(currentPage, newPage);
        Assert.Equal(pagesCount + 1, browser.Pages.Count);
    }
    
    [Fact]
    public async Task RemovePage_CreateAndRemove_ShouldBeSuccess()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var pagesCount = browser.Pages.Count;
        IPage removedPage = null;
        
        _disposables.Add(browser.PageRemoved.Subscribe(it =>
        {
            removedPage = it;
        }));

        // Act
        var resultPage = await browser.CreatePage(new PageCreateOptions(new Uri("https://example.com")));
        await browser.RemovePage(resultPage.Id);
        
        // Assert
        Assert.NotNull(removedPage);
        Assert.Equal(pagesCount, browser.Pages.Count);
    }
    
    [Fact]
    public async Task ReloadAsync_NullExceptionOccurred_ShouldBeHandled()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var currentPage = browser.CurrentPage.Value;
        
        if (currentPage is WebPageStub webPage)
            webPage.RaiseException();

        // Act
        await browser.ReloadPage();
        
        // Assert
        Assert.NotNull(browser);
    }
    
    
    [Fact]
    public async Task ReloadAsync_PageExceptionOccurred_ShouldBeHandled()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var currentPage = browser.CurrentPage.Value;
        
        if (currentPage is WebPageStub webPage)
            webPage.RaisePageException();

        // Act
        await browser.ReloadPage();
        
        // Assert
        Assert.NotNull(browser);
    }
    
    
    [Fact]
    public void Reload_NullExceptionOccurred_ShouldBeHandled()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var currentPage = browser.CurrentPage.Value;
        
        if (currentPage is WebPageStub webPage)
            webPage.RaiseException();

        // Act
        browser.Reload();
        
        // Assert
        Assert.NotNull(browser);
    }
    
    [Fact]
    public void Reload_PageExceptionOccurred_ShouldBeHandled()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var currentPage = browser.CurrentPage.Value;
        
        if (currentPage is WebPageStub webPage)
            webPage.RaisePageException();

        // Act
        browser.Reload();
        
        // Assert
        Assert.NotNull(browser);
    }
    


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables.Dispose();
        }
    }
}
