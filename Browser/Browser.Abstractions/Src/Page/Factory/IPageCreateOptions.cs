namespace Browser.Abstractions.Page.Factory;

public interface IPageCreateOptions
{
    Uri Source { get; }
    
    bool SetActive { get; }
}