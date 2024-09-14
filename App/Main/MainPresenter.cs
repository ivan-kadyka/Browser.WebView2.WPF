using PresenterBase.Presenter;
using WpfApp1.Main;
using WpfApp1.Page;
using WpfApp1.TopPanel;

namespace WpfApp1.Presenters;

public class MainPresenter : PresenterBase<MainWindow>
{
    private readonly IPresenter _topPanelPresenter;
    private readonly IPresenter _pagePresenter;

    public MainPresenter(MainWindow view, MainViewModel viewModel) : base(view, viewModel)
    {
        // Instantiate the child TopPanelPresenter
        var topPanelViewModel = new TopPanelViewModel();
        _topPanelPresenter = new TopPanelPresenter(topPanelViewModel);

        var pageViewModel = new PageViewModel();
        _pagePresenter = new PagePresenter(pageViewModel);
        
        view.TopPanelContent.Content = _topPanelPresenter.View;
        view.PageContent.Content = _pagePresenter.View;
    }
    
    /*
    public async override Task Show(CancellationToken token = default)
    {
        await _topPanelPresenter.Show(token);
        await base.Show(token); 
    }
    */
}