namespace Browser.Abstractions.Page;

public interface IBrowserPageFactory
{
    IBrowserPage Create(IPageCreateOptions options);
}