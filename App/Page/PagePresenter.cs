namespace WpfApp1.Page;
using PresenterBase.Presenter;

internal class PagePresenter : PresenterBase<PageView, PageViewModel>
{
    public PagePresenter(PageViewModel viewModel) : base(viewModel)
    {
    }
}