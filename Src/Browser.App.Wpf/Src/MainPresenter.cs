using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions;
using Browser.Abstractions.Page;
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
        IPresenter pagePresenter,
        IBrowserObservable browserObservable)
    {
        _view = view;
        
        _topPanelPresenter = topPanelPresenter;
        _pagePresenter = pagePresenter;

        _view.DataContext = viewModel;
        _view.SetTopPanelContent(topPanelPresenter.Content);
        _view.SetPageContent(pagePresenter.Content);
        
        AddDisposable(browserObservable.PageRemoved.Subscribe(_ => OnPagesChanged(browserObservable.Pages)));
    }

    private void OnPagesChanged(IReadOnlyList<IPage> pages)
    {
        if (pages.Count == 0)
            _view.Close();
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