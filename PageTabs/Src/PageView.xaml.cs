using System.Windows;
using Microsoft.Web.WebView2.Core;
using PresenterBase.View;
internal partial class PageView : UserControlView
{
    public PageView()
    {
        InitializeComponent();
        InitializeWebView2();
    }
    
    private async void InitializeWebView2()
    {
        // Ensure WebView2 is initialized
        await webView.EnsureCoreWebView2Async(null);

        // Navigate to a URL
        webView.CoreWebView2.Navigate("https://www.onliner.by");

        // Subscribe to navigation events
        //webView.CoreWebView2.NavigationCompleted += OnNavigationCompleted;
    }

    private void OnNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        // Check if navigation was successful
        if (e.IsSuccess)
        {
            MessageBox.Show("Page loaded successfully.");
        }
        else
        {
            MessageBox.Show("Page failed to load.");
        }
    }
}