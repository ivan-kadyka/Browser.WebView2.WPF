using Microsoft.Web.WebView2.Wpf;

namespace Browser.Page.Wpf.Factory;

internal interface IWebViewFactory
{
    IWebView2 Create();
}