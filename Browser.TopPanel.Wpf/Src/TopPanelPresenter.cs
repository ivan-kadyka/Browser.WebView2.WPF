using Browser.Messenger;
using Browser.Messenger.Navigation;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.Presenter;

namespace Browser.TopPanel.Wpf;

internal class TopPanelPresenter : Presenter
{
    private readonly TopPanelViewModel _viewModel;
    private readonly IMessenger _messenger;

    public TopPanelPresenter(TopPanelViewModel viewModel, IMessenger messenger) : base(new TopPanelView(), viewModel)
    {
        _viewModel = viewModel;
        _messenger = messenger;
        
        var navigationViewModel = viewModel.NavigationPanelViewModel;
        
        _messenger.Register<BrowserForwardMessage>(navigationViewModel);
        _messenger.Register<BrowserBackMessage>(navigationViewModel);
        _messenger.Register<BrowserReloadPageMessage>(navigationViewModel);
        _messenger.Register<BrowserActivePageChangedMessage>(navigationViewModel);
        _messenger.Register<NavigationStartingMessage>(navigationViewModel);
        _messenger.Register<NavigationCompletedMessage>(navigationViewModel);
        
        _messenger.Register(viewModel);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        if (disposing)
        {
            _messenger.UnregisterAll(_viewModel);
            _messenger.UnregisterAll(_viewModel.NavigationPanelViewModel);
        }
    }
}