﻿using Browser.Abstractions.Page.Factory;
using Microsoft.Web.WebView2.Wpf;

namespace Browser.WebPage.Wpf.Factory;

internal class WebViewFactory : IWebViewFactory
{
    public IWebView2 Create(IPageCreateOptions options)
    {
        var userDataFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
            System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location)
           );
        
        var webView = new WebView2()
            { CreationProperties = new CoreWebView2CreationProperties() { UserDataFolder = userDataFolder } };
        
        return webView;
    }
}

