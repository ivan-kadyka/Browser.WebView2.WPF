using System;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using PresenterBase.Presenter;

namespace Browser.WebPage.Wpf.Presenters.Container;

internal class PageContainerPresenter : Presenter
{
    public override object Content => _view;
    
    private readonly PageContainerView _view;
    private readonly PageContainerViewModel _viewModel;
    private readonly IBrowser _browser;
    public PageContainerPresenter(IBrowser browser)
    {
        _browser = browser;
        _viewModel = new PageContainerViewModel(browser.CurrentPage.Value);
        _view = new PageContainerView { DataContext = _viewModel };

        AddDisposable(browser.CurrentPage.Subscribe(OnCurrentPageChanged));
    }
    
    private async void OnCurrentPageChanged(IPage page)
    {
        _viewModel.WebContent = page.Content;
        await _browser.LoadPage(page.Id, Token);
    }
}

