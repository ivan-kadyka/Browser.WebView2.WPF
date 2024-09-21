namespace PresenterBase.Presenter;

/// <summary>
/// Defines the base interface for presenters, managing content and lifecycle methods. 
/// Implementers are responsible for handling their disposable resources and controlling the presentation's starting and stopping behavior.
/// </summary>
public interface IPresenter : IDisposable
{
    /// <summary>
    /// Gets the content associated with this presenter. Typically, this is a view or a model that the presenter controls.
    /// </summary>
    object Content { get; }
    
    /// <summary>
    /// Starts the presenter asynchronously. Can be canceled via the provided token.
    /// </summary>
    /// <param name="token">Optional cancellation token to cancel the start process.</param>
    /// <returns>A task representing the asynchronous start operation.</returns>
    Task Start(CancellationToken token = default);
    
    /// <summary>
    /// Stops the presenter asynchronously. Can be canceled via the provided token.
    /// </summary>
    /// <param name="token">Optional cancellation token to cancel the stop process.</param>
    /// <returns>A task representing the asynchronous stop operation.</returns>
    Task Stop(CancellationToken token = default);
}





