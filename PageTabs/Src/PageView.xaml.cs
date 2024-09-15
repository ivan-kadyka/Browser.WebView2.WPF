using System.Threading.Tasks;
using PresenterBase.View;
internal partial class PageView : UserControlView
{
    public PageView()
    {
        InitializeComponent();
    }
    
    
    public void Navigate(string url)
    {
        webView.CoreWebView2.Navigate("https://www."+ url);
    }

    public async Task Start()
    {
        await webView.EnsureCoreWebView2Async(null);
    }
}