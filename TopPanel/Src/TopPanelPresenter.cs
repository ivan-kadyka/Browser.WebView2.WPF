using PresenterBase.Presenter;
using TopPanel;

internal class TopPanelPresenter : PresenterBase<TopPanelView>
{
    public TopPanelPresenter(TopPanelViewModel viewModel) : base(new TopPanelView(), viewModel)
    {
    }
}