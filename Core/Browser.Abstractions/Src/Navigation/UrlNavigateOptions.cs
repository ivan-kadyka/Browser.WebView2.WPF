namespace Browser.Abstractions.Navigation;

public class UrlNavigateOptions : INavigateOptions
{
    public string Address { get; }
    
    public UrlNavigateOptions(string address)
    {
        // TODO: check if it is a valid url
        if (address.StartsWith("http"))
            Address = address;
        else
            Address = "https://"+address;
    }
}