using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;

namespace Browser.Settings;

internal class BrowserPageSettings : IBrowserPageSettings
{
    public Uri Source { get; }
    
    public BrowserPageSettings(IPageCreateOptions options)
    {
        Source = options.Source;
    }
}