namespace Browser.Abstractions.Navigation;

public class NavigateOptions : INavigateOptions
{
    public string Address { get; }
    
    public NavigateOptions(string address)
    {
        Address = address;
    }
}