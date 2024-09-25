using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page.Factory;
using Browser.Settings.Abstractions;
using Browser.TopPanel.Wpf.Module;
using Browser.WebPage.Wpf.Factory;
using Browser.WebPage.Wpf.Module;
using BrowserApp;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Wpf;
using PresenterBase.View;

namespace Browser.App.Tests;

public class BrowserAppTests : IClassFixture<AppServiceFixture>
{
    private readonly AppServiceFixture _appService;

    public BrowserAppTests(AppServiceFixture appService)
    {
        _appService = appService;
    
    }
    
    [Fact]
    public void Browser_CurrentPage_ShouldBeHomePage()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var settings = _appService.GetService<IBrowserSettings>();

        // Act

        
        // Assert
        var homeAddress = settings.General.HomeAddress.Trim('/');
        var currentPageAddress = browser.CurrentPage.Value.Source.Value.ToString().Trim('/');
        Assert.Equal(homeAddress, currentPageAddress);
    }
    
    /*
    [Fact]
    public void Browser_CurrentPage_ShouldBeHomePage_WhenNavigateToHome()
    {
        // Arrange
        var browser = _appService.GetService<IBrowser>();
        var settings = _appService.GetService<IBrowserSettings>();
        var newAddress = "example.com";

        // Act
        browser.Navigate(newAddress);
        
        // Assert
        var currentPageAddress = browser.CurrentPage.Value.Source.Value.ToString().Trim('/');
        Assert.Equal(newAddress, currentPageAddress);
    }
    */
}

internal class WebViewFactoryMock : IWebViewFactory
{
    public IWebView2 Create(IPageCreateOptions options)
    {
        return Substitute.For<IWebView2>();
    }
}