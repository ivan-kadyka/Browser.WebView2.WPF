using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;

namespace Browser.Settings;

internal class BrowserSettings : IBrowserSettings
{
    public IBrowserPageSettings CreatePage(IPageCreateOptions options)
    {
        return new BrowserPageSettings(options);
    }
}