using Reactive.Extensions.Observable;

namespace Browser.Abstractions.Navigation;

/// <summary>
/// Defines an observable path interface that provides access to the current source URI through an observable value.
/// </summary>
public interface IPathObservable
{
    /// <summary>
    /// Gets an observable value representing the current source URI.
    /// </summary>
    IObservableValue<Uri> Source { get; }
}