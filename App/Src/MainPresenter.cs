using System.Threading;
using System.Threading.Tasks;
using BrowserApp;
using PresenterBase.Presenter;

internal class MainPresenter : Presenter
{
    private readonly IPresenter _topPanelPresenter;
    private readonly IPresenter _pagePresenter;

    public MainPresenter(MainWindow view,
        MainViewModel viewModel,
        IPresenter topPanelPresenter,
        IPresenter pagePresenter) : base(view, viewModel)
    {
        _topPanelPresenter = topPanelPresenter;
        _pagePresenter = pagePresenter;

        view.TopPanel.Content = topPanelPresenter.Content;
        view.Page.Content = pagePresenter.Content;
    }
    
    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
        await _topPanelPresenter.Start(token);
        await _pagePresenter.Start(token);
    }

    protected override async Task OnStopped(CancellationToken token = default)
    {
        await _pagePresenter.Stop(token);
        await _topPanelPresenter.Stop(token);
        
        await base.OnStopped(token);
    }
}