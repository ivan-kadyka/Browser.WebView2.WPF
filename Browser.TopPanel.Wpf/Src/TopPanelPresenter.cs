using System.Threading;
using System.Threading.Tasks;
using PresenterBase.Presenter;

namespace Browser.TopPanel.Wpf;

internal class TopPanelPresenter : Presenter
{
    public override object Content => _view;
    
    private readonly TopPanelViewModel _viewModel;
    private readonly TopPanelView _view;

    public TopPanelPresenter(TopPanelViewModel viewModel)
    {
        _view = new TopPanelView();
        _viewModel = viewModel;
        _view.DataContext = _viewModel;
    }

    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
        _viewModel.NavigationPanelViewModel.IsActive = true;
        _viewModel.SearchBarViewModel.IsActive = true;
    }

    protected override async Task OnStopped(CancellationToken token = default)
    {
        _viewModel.NavigationPanelViewModel.IsActive = false;
        _viewModel.SearchBarViewModel.IsActive = false;
        
        await base.OnStopped(token);
    }
}