using PresenterBase.Presenter;

public class PagePresenter : PresenterBase<PageView>
{
    public PagePresenter(PageViewModel viewModel) : base(new PageView(), viewModel)
    {
    }
}