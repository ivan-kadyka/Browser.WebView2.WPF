using System.Text.RegularExpressions;
using Browser.Settings.Abstractions;

namespace Browser.Core.UriResolver;

internal class BrowserUriResolver : IUriResolver
{
    private readonly IBrowserSettings _browserSettings;

    private static readonly Regex DomainNameRegex = new Regex(@"^(?!-)[A-Za-z0-9-]{1,63}(?<!-)\.[A-Za-z]{2,6}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    public BrowserUriResolver(IBrowserSettings browserSettings)
    {
        _browserSettings = browserSettings;
    }
    
    public  Uri GetUri(string address)
    {
        string currentAddress = EnsureScheme(address);
        
        if (Uri.TryCreate(currentAddress, UriKind.Absolute, out var uri))
        {
            if (DomainNameRegex.IsMatch(uri.Host))
            {
                return uri;
            }
        }
        
        
        var formattedAddress = address.Replace(" ", "+");
        formattedAddress = string.Format(_browserSettings.General.SearchAddress, formattedAddress);
        return new Uri(formattedAddress);
    }
    
    private string EnsureScheme(string address)
    {
        if (!address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !address.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return "https://" + address;
        }
        return address;
    }
}