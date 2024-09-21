namespace Browser.Abstractions.Navigation;

/// <summary>
/// Represents navigation options for specifying the address or path to navigate to.
/// </summary>
public interface INavigateOptions
{
    /// <summary>
    /// Gets the address or path for navigation.
    /// </summary>
    string Address { get; }
}