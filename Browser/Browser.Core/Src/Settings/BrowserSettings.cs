using Browser.Abstractions.Page;

namespace Browser.Core.Settings;

internal class BrowserSettings : IBrowserSettings
{
    public IBrowserPageSettings CreatePage(IPageCreateOptions options)
    {
        return new BrowserPageSettings(options);
    }
}