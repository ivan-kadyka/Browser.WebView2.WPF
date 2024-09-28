using System.Reactive.Disposables;
using Browser.Abstractions;
using BrowserApp;
using BrowserApp.Main;
using Disposable;

namespace Browser.App.Tests.Presenters;

public class MainPresenterTests : DisposableBase, IClassFixture<AppServiceFixture>
{
    private readonly AppServiceFixture _appService;
    private readonly CompositeDisposable _disposables = new();

    public MainPresenterTests(AppServiceFixture appService)
    {
        _appService = appService;
    }


    [Fact]
    public void Constructor_ShouldBeSuccess()
    {
        // Arrange
        var mainPresenter = _appService.GetService<MainPresenter>();
        
        // Assert
        Assert.NotNull(mainPresenter);
        Assert.NotNull(mainPresenter.Content);
    }
    
    [Fact]
    public async Task RemovePage_LastPage_MainWindowShouldBeClosed()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var mainWindow = _appService.GetService<IMainWindow>();
        var currentPage = browser.CurrentPage.Value;
        
        // Act
        await browser.RemovePage(currentPage.Id);
        
        // Assert
        mainWindow.Received().Close();
    }


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables.Dispose();
        }
    }
}