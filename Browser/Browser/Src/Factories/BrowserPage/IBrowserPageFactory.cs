using Browser.Abstractions;
using Browser.Abstractions.Navigation;

namespace Browser.Factories.BrowserPage;

public interface IBrowserPageFactory
{
    IBrowserPage Create(INavigateOptions options);
}