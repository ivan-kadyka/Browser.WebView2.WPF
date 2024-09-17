using Browser.Abstractions;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Presenters.Container;

internal class PageContainerPresenter : Presenter
{
    public PageContainerPresenter(IBrowser browser) 
        : base(new PageContainerView(),  new PageContainerViewModel(browser))
    {
    }
}

