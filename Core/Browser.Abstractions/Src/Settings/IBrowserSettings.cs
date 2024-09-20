using Browser.Abstractions.Page.Factory;

namespace Browser.Abstractions.Settings;

public interface IBrowserSettings
{
    IBrowserPageSettings CreatePage(IPageCreateOptions options);
}