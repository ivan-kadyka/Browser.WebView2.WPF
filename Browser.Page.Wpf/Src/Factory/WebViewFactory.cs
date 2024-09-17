using Microsoft.Web.WebView2.Wpf;

namespace Browser.Page.Wpf.Factory;

internal class WebViewFactory : IWebViewFactory
{
    public IWebView2 Create()
    {
        var userDataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
            System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location)
           );
        
        WebView2 webView = new WebView2()
            { CreationProperties = new CoreWebView2CreationProperties() { UserDataFolder = userDataFolder } };
        
        return webView;
    }
}

