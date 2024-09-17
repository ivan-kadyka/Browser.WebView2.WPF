using System.Windows.Input;
using Browser.Abstractions.Navigation;
using Browser.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.ViewModel;
using TopPanel.TabsPanel;

namespace TopPanel;

internal class TopPanelViewModel : ViewModelBase,
    IRecipient<BrowserForwardMessage>,
    IRecipient<BrowserBackMessage>,
    IRecipient<BrowserRefreshMessage>,
    IRecipient<NavigationPathChangedMessage>
{
    public TabsPanelViewModel TabsPanelViewModel { get; }
    
    private readonly IBrowserRouter _browserRouter;
    
    private string _searchAddress;
    public string SearchAddress
    {
        get => _searchAddress;
        set => SerProperty(ref _searchAddress, value);
    }
    
    public ICommand SearchCommand { get; }
    
    public ICommand BackCommand => _backCommand;
    public ICommand ForwardCommand => _forwardCommand;
    public ICommand RefreshCommand => _refreshCommand;
    
    private readonly RelayCommand _backCommand;
    private readonly RelayCommand _forwardCommand;
    private readonly RelayCommand _refreshCommand;

    public TopPanelViewModel(IBrowserRouter browserRouter, TabsPanelViewModel tabsPanelViewModel)
    {
        TabsPanelViewModel = tabsPanelViewModel;
        
        _searchAddress = string.Empty;
        _browserRouter = browserRouter;
        
        SearchCommand = new RelayCommand(OnSearch);
        
        _backCommand = new RelayCommand(_browserRouter.Back, ()=> browserRouter.CanBack);
        _forwardCommand = new RelayCommand(_browserRouter.Forward, ()=> browserRouter.CanForward);
        _refreshCommand = new RelayCommand(_browserRouter.Refresh, ()=>browserRouter.CanRefresh);
    }

    private void OnSearch()
    {
        _browserRouter.Navigate(SearchAddress);
        
        _backCommand.NotifyCanExecuteChanged();
        _forwardCommand.NotifyCanExecuteChanged();
        _refreshCommand.NotifyCanExecuteChanged();
    }

    public void Receive(BrowserForwardMessage message)
    {
        _forwardCommand.NotifyCanExecuteChanged();
        _backCommand.NotifyCanExecuteChanged();
    }

    public void Receive(BrowserBackMessage message)
    {
        _forwardCommand.NotifyCanExecuteChanged();
        _backCommand.NotifyCanExecuteChanged();
    }

    public void Receive(BrowserRefreshMessage message)
    {
        _refreshCommand.NotifyCanExecuteChanged();
    }

    public void Receive(NavigationPathChangedMessage message)
    {
        SearchAddress = message.Address;
    }
}