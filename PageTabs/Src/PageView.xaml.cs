using System.Threading;
using System.Threading.Tasks;
using PresenterBase.View;
internal partial class PageView : UserControlView
{
    public PageView()
    {
        InitializeComponent();
    }
    
    protected override async Task OnShow(CancellationToken token = default)
    {
        await WebView.EnsureCoreWebView2Async(null);
    }
    
    public void Navigate(string url)
    {
         WebView.CoreWebView2.Navigate("https://www."+ url);
    }
}