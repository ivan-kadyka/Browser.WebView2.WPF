namespace Browser.Abstractions.Navigation;

/// <summary>
/// Represents navigation options, such as the address to navigate to.
/// </summary>
public class NavigateOptions : INavigateOptions
{
    /// <summary>
    /// Gets the address or path to navigate to.
    /// </summary>
    public string Address { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigateOptions"/> class with the specified address.
    /// </summary>
    /// <param name="address">The address to navigate to.</param>
    public NavigateOptions(string address)
    {
        Address = address;
    }
}