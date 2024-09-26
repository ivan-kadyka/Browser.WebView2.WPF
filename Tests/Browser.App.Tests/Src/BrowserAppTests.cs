using System.Reactive.Disposables;
using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Disposable;

namespace Browser.App.Tests;

public class BrowserAppTests : DisposableBase,IClassFixture<AppServiceFixture>
{
    private readonly AppServiceFixture _appService;
    private readonly CompositeDisposable _disposables = new();

    public BrowserAppTests(AppServiceFixture appService)
    {
        _appService = appService;
    }
    
    
    [Theory]
    [InlineData("https://example.com")]
    [InlineData("https://duckduckgo.com")]
    public void Browser_Navigate_CurrentPageShouldBeNewPage(string newAddress)
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
        var currentPageUri = browser.CurrentPage.Value.Source.Value;
        Assert.Equal(currentPageUri, newUri);
        
        Assert.NotNull(raisedUri);
        Assert.Equal(raisedUri, newUri);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables.Dispose();
        }
    }
}
