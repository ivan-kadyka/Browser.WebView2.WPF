using PresenterBase.Presenter;

public class TopPanelPresenter : PresenterBase<TopPanelView>
{
    public TopPanelPresenter(TopPanelViewModel viewModel) : base(new TopPanelView(), viewModel)
    {
    }
}