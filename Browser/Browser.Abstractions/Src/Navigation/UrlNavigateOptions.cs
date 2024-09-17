namespace Browser.Abstractions.Navigation;

public class UrlNavigateOptions : INavigateOptions
{
    public string Address { get; }
    
    public UrlNavigateOptions(string address)
    {
        Address = address;
    }
}