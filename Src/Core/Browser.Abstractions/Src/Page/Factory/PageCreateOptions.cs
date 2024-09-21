namespace Browser.Abstractions.Page.Factory;

public class PageCreateOptions : IPageCreateOptions
{
    public Uri Source { get; }
    
    public bool SetActive { get; }

    public PageCreateOptions(Uri source, bool setActive = true)
    {
        Source = source;
        SetActive = setActive;
    }
}