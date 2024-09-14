using System.Threading;
using System.Threading.Tasks;
using PresenterBase.View;
using PresenterBase.ViewModel;

namespace PresenterBase.Presenter;

public abstract class PresenterBase<TView, TViewModel> : IPresenter
    where TView : class, IView, new()
    where TViewModel : class, IViewModel
{
    public IView View { get; }
    
    protected TView TypeView { get; private set; }
    
    protected TViewModel ViewModel { get; }

    protected PresenterBase(TView view, TViewModel viewModel)
    {
        View = view;
        TypeView = view;
        ViewModel = viewModel;
        
        View.DataContext = ViewModel;
    }

    protected PresenterBase(TViewModel viewModel) : this(new TView(), viewModel)
    {
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
