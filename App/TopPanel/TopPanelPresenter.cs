using PresenterBase.Presenter;

namespace WpfApp1.TopPanel;
 
public class TopPanelPresenter : PresenterBase<TopPanelView, TopPanelViewModel>
{
    public TopPanelPresenter(TopPanelViewModel viewModel) : base(viewModel)
    {
    }
}