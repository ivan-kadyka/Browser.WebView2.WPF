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
    }

    public async Task Stop(CancellationToken token = default)
    {
        await View.Hide(token);
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
