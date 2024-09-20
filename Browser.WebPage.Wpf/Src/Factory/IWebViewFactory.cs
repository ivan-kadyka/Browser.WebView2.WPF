using Browser.Abstractions.Page.Factory;
using Microsoft.Web.WebView2.Wpf;

namespace Browser.WebPage.Wpf.Factory;

internal interface IWebViewFactory
{
    IWebView2 Create(IPageCreateOptions options);
}