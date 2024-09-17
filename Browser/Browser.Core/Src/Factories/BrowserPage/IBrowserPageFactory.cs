using Browser.Abstractions;
using Browser.Abstractions.Navigation;

namespace Browser.Core.Factories.BrowserPage;

public interface IBrowserPageFactory
{
    IBrowserPage Create(INavigateOptions options);
}