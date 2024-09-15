using PresenterBase.Presenter;
using TopPanel;

internal class TopPanelPresenter : Presenter
{
    public TopPanelPresenter(TopPanelViewModel viewModel) : base(new TopPanelView(), viewModel)
    {
    }
}