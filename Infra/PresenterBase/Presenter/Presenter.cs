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
    public IView View { get; }

    private readonly CompositeDisposable _disposables = new();

    protected Presenter(IView view, IViewModel viewModel)
    {
        View = view;
        View.DataContext = viewModel;
    }
    
    
    public async Task Start(CancellationToken token = default)
    {
        await View.Show(token);
        await OnStarted(token);
    }
    
    
    public async Task Stop(CancellationToken token = default)
    {
        await View.Hide(token);
        await OnStopped(token);
    }
    
    
    protected virtual Task OnStarted(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
    
    protected virtual Task OnStopped(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
    
    protected void AddDisposable(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

   protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables.Dispose();
        }
    }
}
