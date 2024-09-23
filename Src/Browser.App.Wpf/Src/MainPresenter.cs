using System.Threading;
using System.Threading.Tasks;
using PresenterBase.Presenter;

namespace BrowserApp;

internal class MainPresenter : Presenter
{
    private readonly IPresenter _topPanelPresenter;
    private readonly IPresenter _pagePresenter;
    
    public override object Content => _view;
    
    private readonly IMainWindow _view;

    public MainPresenter(
        MainViewModel viewModel,
        IMainWindow view,
        IPresenter topPanelPresenter,
        IPresenter pagePresenter)
    {
        _view = view;
        
        _topPanelPresenter = topPanelPresenter;
        _pagePresenter = pagePresenter;

        _view.DataContext = viewModel;
        _view.SetTopPanelContent(topPanelPresenter.Content);
        _view.SetPageContent(pagePresenter.Content);
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