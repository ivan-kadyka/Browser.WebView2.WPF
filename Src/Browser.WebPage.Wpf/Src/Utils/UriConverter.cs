using System;
using Browser.Settings.Abstractions;

namespace Browser.WebPage.Wpf.Utils;

internal class UriConverter
{
    private readonly IBrowserSettings _browserSettings;

    public UriConverter(IBrowserSettings browserSettings)
    {
        _browserSettings = browserSettings;
    }
    
    public  Uri ToUri(string address)
    {
        string currentAddress = address;
       
        if (!address.StartsWith("http://") && !address.StartsWith("https://"))
        {
            currentAddress = "https://" + address;
        }
        
        if (Uri.TryCreate(currentAddress, UriKind.Absolute, out var uri))
        {
            return uri;
        }
        else
        {
            return new Uri(_browserSettings.General.SearchAddress + address);
        }
    }
}