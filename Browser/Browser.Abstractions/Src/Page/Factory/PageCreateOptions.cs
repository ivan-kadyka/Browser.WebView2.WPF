namespace Browser.Abstractions.Page;

public class PageCreateOptions : IPageCreateOptions
{
    public Uri Source { get; }
    
    public PageCreateOptions(Uri source)
    {
        Source = source;
    }
    
    public PageCreateOptions(string source) : this(new Uri(source))
    {
    }
}