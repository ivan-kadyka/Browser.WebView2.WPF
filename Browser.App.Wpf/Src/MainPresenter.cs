using System.Threading;
using System.Threading.Tasks;
using PresenterBase.Presenter;

namespace BrowserApp;

internal class MainPresenter : Presenter
{
    private readonly IPresenter _topPanelPresenter;
    private readonly IPresenter _pagePresenter;
    
    public override object Content => _view;
    
    private readonly MainWindow _view;

    public MainPresenter(
        MainViewModel viewModel,
        IPresenter topPanelPresenter,
        IPresenter pagePresenter)
    {
        _view = new MainWindow();
        
        _topPanelPresenter = topPanelPresenter;
        _pagePresenter = pagePresenter;

        _view.DataContext = viewModel;
        _view.TopPanel.Content = topPanelPresenter.Content;
        _view.Page.Content = pagePresenter.Content;
    }
    
    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
        await _topPanelPresenter.Start(token);
        await _pagePresenter.Start(token);
        
        _view.Show();
    }

    protected override async Task OnStopped(CancellationToken token = default)
    {
        await _pagePresenter.Stop(token);
        await _topPanelPresenter.Stop(token);
        
        await base.OnStopped(token);
    }
}