namespace Browser.Abstractions.Settings;

/// <summary>
/// Defines the settings for a browser page, including the source URI.
/// </summary>
public interface IBrowserPageSettings
{
    /// <summary>
    /// Gets the source URI for the browser page.
    /// </summary>
    Uri Source { get; }
}