using System;

namespace Browser.WebPage.Wpf.Utils;

internal static class UriConverter
{
    public static Uri ToUri(string address)
    {
        if (!address.StartsWith("http://") && !address.StartsWith("https://"))
        {
            address = "https://" + address;
        }
        
        var uri = new Uri(address);
        return uri;
    }
}