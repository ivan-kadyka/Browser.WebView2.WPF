using System.Reactive.Disposables;
using Browser.Abstractions;
using Browser.Abstractions.Exceptions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.App.Tests.Utils;
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
        Uri? raisedUri = default;
        
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
        IPage? reloadedPage = default;
        
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
        IPage? newPage = default;
        
        _disposables.Add(browser.PageAdded.Subscribe(it =>
        {
            newPage = it;
        }));

        // Act
        await browser.CreatePage(new PageCreateOptions(new Uri("https://example.com")));
        
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
        IPage? removedPage = default;
        
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
    
    [Theory]
    [InlineData(null)]
    [InlineData(PageError.Reload)]
    public async Task ReloadAsync_ExceptionOccurred_ShouldBeHandled(PageError? error)
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var testExceptionProxy = _appService.GetService<TestExceptionProxy>();
        
        testExceptionProxy.PrepareRaiseException(error);

        // Act
        await browser.ReloadPage();
        
        // Assert
        Assert.NotNull(browser);
    }
    
    
    [Theory]
    [InlineData(null)]
    [InlineData(PageError.Reload)]
    public void Reload_ExceptionOccurred_ShouldBeHandled(PageError? error)
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var testExceptionProxy = _appService.GetService<TestExceptionProxy>();
        
        testExceptionProxy.PrepareRaiseException(error);

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
