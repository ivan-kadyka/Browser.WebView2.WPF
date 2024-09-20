namespace Browser.Abstractions.Page;

public interface IBrowserSettings
{
    IBrowserPageSettings CreatePage(IPageCreateOptions options);
}