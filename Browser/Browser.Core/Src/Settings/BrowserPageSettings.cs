using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;

namespace Browser.Core.Settings;

public class BrowserPageSettings : IBrowserPageSettings
{
    public Uri Source { get; }
    
    public BrowserPageSettings(IPageCreateOptions options)
    {
        Source = options.Source;
    }
}