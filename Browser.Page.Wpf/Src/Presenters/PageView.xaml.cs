using System.Threading;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Wpf;
using PresenterBase.View;

namespace Browser.Page.Wpf.Presenters;

internal partial class PageView : UserControlView
{
    private readonly WebView2 _webView;
    public PageView()
    {
        InitializeComponent();
        
        _webView= new WebView2();
        WebView.Content = _webView;
    }
    
    protected override async Task OnShow(CancellationToken token = default)
    {
        await _webView.EnsureCoreWebView2Async(null);
    }
    
    public void Navigate(string url)
    {
        _webView.CoreWebView2.Navigate("https://"+ url);
    }
}