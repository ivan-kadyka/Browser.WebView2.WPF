using Browser.Abstractions;
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
        _viewModel = new PageContainerViewModel(browser);
        
        _view = new PageContainerView();
        _view.DataContext = _viewModel;
    }
}

