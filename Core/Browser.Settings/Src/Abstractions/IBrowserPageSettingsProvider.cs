using Browser.Abstractions.Page.Factory;
using Browser.Abstractions.Settings;

namespace Browser.Settings.Abstractions;

public interface IBrowserPageSettingsProvider
{
    IBrowserPageSettings Get(IPageCreateOptions options);
}