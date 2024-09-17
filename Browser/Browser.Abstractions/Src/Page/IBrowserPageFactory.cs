using Browser.Abstractions.Navigation;

namespace Browser.Abstractions.Page;

public interface IBrowserPageFactory
{
    IBrowserPage Create(INavigateOptions options);
}