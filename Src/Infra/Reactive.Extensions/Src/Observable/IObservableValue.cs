namespace Reactive.Extensions.Observable;

/// <summary>
/// Extends the IObservable interface to include a property that synchronously exposes the current value of the observable sequence.
/// </summary>
/// <typeparam name="TResult">The type of element provided by the observable sequence.</typeparam>
public interface IObservableValue<out TResult> : IObservable<TResult>
{
    /// <summary>
    /// Gets the current value of the observable sequence. 
    /// This property provides synchronous access to the value, allowing for immediate retrieval without waiting for a new value to be emitted.
    /// </summary>
    TResult Value { get; }
}
