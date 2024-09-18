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
        
        _messenger.Register<BrowserForwardMessage>(viewModel);
        _messenger.Register<BrowserBackMessage>(viewModel);
        _messenger.Register<BrowserReloadPageMessage>(viewModel);
        
        _messenger.Register<BrowserActivePageChangedMessage>(viewModel);
        _messenger.Register<BrowserSearchAddressChangedMessage>(viewModel);
        
        _messenger.Register<NavigationStartingMessage>(viewModel);
        _messenger.Register<NavigationCompletedMessage>(viewModel);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        if (disposing)
        {
            _messenger.UnregisterAll(_viewModel);
        }
    }
}