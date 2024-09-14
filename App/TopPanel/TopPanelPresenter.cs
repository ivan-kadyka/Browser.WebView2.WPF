using PresenterBase.Presenter;

namespace WpfApp1.TopPanel;
 
public class TopPanelPresenter : PresenterBase<TopPanelView>
{
    public TopPanelPresenter(TopPanelViewModel viewModel) : base(new TopPanelView(), viewModel)
    {
    }
}