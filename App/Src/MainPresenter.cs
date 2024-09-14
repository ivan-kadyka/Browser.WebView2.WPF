using BrowserApp;
using PresenterBase.Presenter;

internal class MainPresenter : PresenterBase<MainWindow>
{
    public MainPresenter(MainWindow view,
        MainViewModel viewModel,
        IPresenter topPanelPresenter,
        IPresenter pagePresenter) : base(view, viewModel)
    {
        view.TopPanel.Content = topPanelPresenter.View;
        view.Page.Content = pagePresenter.View;
    }
}