using Browser.Abstractions;
using Browser.Abstractions.Page;
using Browser.TopPanel.Wpf;

namespace Browser.App.Tests.TopPanel;

public class TabsPanelViewModelTests :  IClassFixture<AppServiceFixture>
{
    private readonly AppServiceFixture _appService;

    private readonly TopPanelViewModel _topPanelViewModel;

    public TabsPanelViewModelTests(AppServiceFixture appService)
    {
        _appService = appService;
        _topPanelViewModel = appService.GetService<TopPanelViewModel>();
        
        _topPanelViewModel.SearchBarViewModel.SearchAddress = "https://example.com";
    }
    
    
    [Theory]
    [InlineData("https://example.com")]
    public void Search_SetAddress_ShouldBeBrowserSource(string address)
    {
        // Arrange
        var searchBarViewModel = _topPanelViewModel.SearchBarViewModel;
        var browser = _appService.GetService<IBrowser>();
        
        // Act
        searchBarViewModel.SearchAddress = address;
        searchBarViewModel.SearchCommand.Execute(null);
        
        // Assert
        Assert.Equal(address, browser.Source.Value.ToString().TrimEnd('/'));
    }
    
    [Fact]
    public void AddNewTab_ExecuteCommand_ShouldBePageAdded()
    {
        // Arrange
        var tabsPanelViewModel = _topPanelViewModel.TabsPanelViewModel;
        var browser = _appService.GetService<IBrowser>();
        var pagesCount = browser.Pages.Count;
        IPage? newPage = null;
        
        browser.PageAdded.Subscribe(it =>
        {
            newPage = it;
        });
        
        // Act
        tabsPanelViewModel.CreateTabCommand.Execute(null);
        
        // Assert
        Assert.NotNull(newPage);
        Assert.Equal(pagesCount + 1, browser.Pages.Count);
    }
    
    [Fact]
    public void RemoveTab_ExecuteCommand_ShouldBePageRemoved()
    {
        // Arrange
        var tabsPanelViewModel = _topPanelViewModel.TabsPanelViewModel;
        var browser = _appService.GetService<IBrowser>();
        
        tabsPanelViewModel.CreateTabCommand.Execute(null);
        
        var pagesCount = browser.Pages.Count;
        IPage? removedPage = null;
        
        browser.PageRemoved.Subscribe(it =>
        {
            removedPage = it;
        });
        
        tabsPanelViewModel.SelectedPageTab = tabsPanelViewModel.Tabs.Last();
        var tabItem = tabsPanelViewModel.SelectedPageTab;

        // Act
        tabsPanelViewModel.RemoveTabCommand.Execute(tabItem);
        
        // Assert
        Assert.NotNull(removedPage);
        Assert.Equal(pagesCount - 1, browser.Pages.Count);
    }

    [Fact]
    public void ReloadTab_ExecuteCommand_ShouldBePageReloaded()
    {
        // Arrange
        var navigationPanelViewModel = _topPanelViewModel.NavigationPanelViewModel;
        var browser = _appService.GetService<IBrowser>();
        IPage? reloadedPage = null;
        
        browser.PageReloaded.Subscribe(it =>
        {
            reloadedPage = it;
        });

        // Act
        navigationPanelViewModel.ReloadCommand.Execute(null);
        
        // Assert
        Assert.NotNull(reloadedPage);
    }
}