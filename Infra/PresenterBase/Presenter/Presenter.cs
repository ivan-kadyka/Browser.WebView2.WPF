using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Disposable;
using PresenterBase.View;
using PresenterBase.ViewModel;

namespace PresenterBase.Presenter;

public abstract class Presenter : DisposableBase, IPresenter
{
    public object Content { get; }
    
    protected IView View { get; }

    private readonly CompositeDisposable _disposables = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    protected Presenter(IView view, IViewModel viewModel)
    {
        View = view;
        Content = view;
        view.DataContext = viewModel;
    }
    
    
    public async Task Start(CancellationToken token = default)
    {
        var linkedToken = GetLinkedToken(token);
        
        await View.Show(linkedToken);
        await OnStarted(linkedToken);
    }
    
    
    public async Task Stop(CancellationToken token = default)
    {
        var linkedToken = GetLinkedToken(token);
        
        await View.Hide(linkedToken);
        await OnStopped(linkedToken);
    }
    
    
    protected virtual Task OnStarted(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
    
    protected virtual Task OnStopped(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
    
    private CancellationToken GetLinkedToken(CancellationToken token)
    {
        return CancellationTokenSource.CreateLinkedTokenSource(token, _cancellationTokenSource.Token).Token;
    }
    
    protected void AddDisposable(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

   protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource.Cancel();
            
            _disposables.Dispose();
        }
    }
}
