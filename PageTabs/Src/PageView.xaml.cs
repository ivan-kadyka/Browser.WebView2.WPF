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
        WebView.CoreWebView2.Navigate("https://www."+ url);
    }

    public async Task Start()
    {
        await WebView.EnsureCoreWebView2Async(null);
    }
}