using PresenterBase.Presenter;

internal class PagePresenter : PresenterBase<PageView>
{
    public PagePresenter(PageViewModel viewModel) : base(new PageView(), viewModel)
    {
    }
}