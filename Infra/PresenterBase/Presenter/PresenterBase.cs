using System.Threading;
using System.Threading.Tasks;
using PresenterBase.View;
using PresenterBase.ViewModel;

namespace PresenterBase.Presenter;

public abstract class PresenterBase<TView> : IPresenter
    where TView : class, IView
{
    public IView View { get; }
    
    protected TView ProtectedView { get; private set; }

    protected PresenterBase(TView view, IViewModel viewModel)
    {
        View = ProtectedView = view;
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
    

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
