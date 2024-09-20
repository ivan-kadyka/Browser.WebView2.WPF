namespace PresenterBase.Presenter;

public interface IPresenter :  IDisposable
{
    object Content { get; }
    
    Task Start(CancellationToken token = default);
    
    
    Task Stop(CancellationToken token = default);
}





