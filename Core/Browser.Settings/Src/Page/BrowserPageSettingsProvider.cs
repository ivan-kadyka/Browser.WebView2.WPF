using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;
using Browser.Settings.Abstractions;

namespace Browser.Settings.Page;

public class BrowserPageSettingsProvider : IBrowserPageSettingsProvider
{
    public IBrowserPageSettings Get(IPageCreateOptions options)
    {
        return new BrowserPageSettings(options);
    }
}