using Browser.Messenger;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.Presenter;
using Browser.TopPanel.Wpf;

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
        _messenger.Register<BrowserRefreshMessage>(viewModel);
        _messenger.Register<NavigationPathChangedMessage>(viewModel);
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