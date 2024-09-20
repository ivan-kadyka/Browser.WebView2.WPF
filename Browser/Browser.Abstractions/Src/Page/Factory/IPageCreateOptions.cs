namespace Browser.Abstractions.Page;

public interface IPageCreateOptions
{
    Uri Source { get; }
    
    bool SetActive { get; }
}