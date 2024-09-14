using PresenterBase.Presenter;

internal class TopPanelPresenter : PresenterBase<TopPanelView>
{
    public TopPanelPresenter(TopPanelViewModel viewModel) : base(new TopPanelView(), viewModel)
    {
    }
}