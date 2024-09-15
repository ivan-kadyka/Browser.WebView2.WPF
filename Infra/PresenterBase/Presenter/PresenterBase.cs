using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Disposable;
using PresenterBase.View;
using PresenterBase.ViewModel;

namespace PresenterBase.Presenter;

public abstract class PresenterBase<TView> : DisposableBase, IPresenter
    where TView : class, IView
{
    public IView View => _view;

    protected readonly TView _view;
    
    private readonly CompositeDisposable _disposables = new();

    protected PresenterBase(TView view, IViewModel viewModel)
    {
        _view = view;
        _view.DataContext = viewModel;
    }
    
    
    public async Task Start(CancellationToken token = default)
    {
        await _view.Show(token);
        await OnStarted(token);
    }
    
    
    public async Task Stop(CancellationToken token = default)
    {
        await _view.Hide(token);
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
