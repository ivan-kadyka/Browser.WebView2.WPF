namespace Browser.Abstractions.Page;

public class PageCreateOptions : IPageCreateOptions
{
    public Uri Source { get; }
    
    public bool SetActive { get; }

    public PageCreateOptions(Uri source, bool setActive = true)
    {
        Source = source;
    }
    
    public PageCreateOptions(string source,  bool setActive = true) 
        : this(new Uri(source), setActive)
    {
    }
}