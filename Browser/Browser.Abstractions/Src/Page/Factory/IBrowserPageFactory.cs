namespace Browser.Abstractions.Page.Factory;

public interface IBrowserPageFactory
{
    IBrowserPage Create(IPageCreateOptions options);
}