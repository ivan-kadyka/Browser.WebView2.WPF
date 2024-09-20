using Browser.Abstractions.Page;

namespace Browser.Core.Settings;

public class BrowserPageSettings : IBrowserPageSettings
{
    public Uri Source { get; }
    
    public BrowserPageSettings(IPageCreateOptions options)
    {
        Source = options.Source;
    }
}