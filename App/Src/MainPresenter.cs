using BrowserApp;
using PresenterBase.Presenter;


public class MainPresenter : PresenterBase<MainWindow>
{
    private readonly IPresenter _topPanelPresenter;
    private readonly IPresenter _pagePresenter;

    public MainPresenter(MainWindow view, MainViewModel viewModel) : base(view, viewModel)
    {
        var topPanelViewModel = new TopPanelViewModel();
        _topPanelPresenter = new TopPanelPresenter(topPanelViewModel);

        var pageViewModel = new PageViewModel();
        _pagePresenter = new PagePresenter(pageViewModel);
        
        view.TopPanel.Content = _topPanelPresenter.View;
        view.Page.Content = _pagePresenter.View;
    }
}