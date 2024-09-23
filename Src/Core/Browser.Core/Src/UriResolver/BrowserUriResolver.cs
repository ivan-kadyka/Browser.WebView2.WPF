using Browser.Settings.Abstractions;

namespace Browser.Core.UriResolver;

internal class BrowserUriResolver : IUriResolver
{
    private readonly IBrowserSettings _browserSettings;

    public BrowserUriResolver(IBrowserSettings browserSettings)
    {
        _browserSettings = browserSettings;
    }
    
    public  Uri GetUri(string address)
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
            var formattedAddress = address.Replace(" ", "+");
            formattedAddress = string.Format(_browserSettings.General.SearchAddress, formattedAddress);
            return new Uri(formattedAddress);
        }
    }
}