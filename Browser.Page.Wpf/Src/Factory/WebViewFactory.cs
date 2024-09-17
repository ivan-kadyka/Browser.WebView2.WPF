using Microsoft.Web.WebView2.Wpf;

namespace Browser.Page.Wpf.Factory;

internal class WebViewFactory : IWebViewFactory
{
    public IWebView2 Create()
    {
        return new WebView2();
    }
}

