using PresenterBase.Presenter;
using WpfApp1.Main;
using WpfApp1.Page;
using WpfApp1.TopPanel;

namespace WpfApp1.Presenters;

public class MainPresenter : PresenterBase<MainWindow, MainViewModel>
{
    private readonly IPresenter _topPanelPresenter;
    private readonly IPresenter _pagePresenter;

    public MainPresenter(MainViewModel viewModel) : base(viewModel)
    {
        // Instantiate the child TopPanelPresenter
        var topPanelViewModel = new TopPanelViewModel();
        _topPanelPresenter = new TopPanelPresenter(topPanelViewModel);

        var pageViewModel = new PageViewModel();
        _pagePresenter = new PagePresenter(pageViewModel);
        
        var mainView = View as MainWindow;
        mainView.TopPanelContent.Content = _topPanelPresenter.View;
        mainView.PageContent.Content = _pagePresenter.View;
    }
    
    /*
    public async override Task Show(CancellationToken token = default)
    {
        await _topPanelPresenter.Show(token);
        await base.Show(token); 
    }
    */
}