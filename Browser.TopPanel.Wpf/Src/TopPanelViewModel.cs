using System.Windows.Input;
using Browser.Abstractions.Navigation;
using Browser.Messenger;
using Browser.Messenger.Navigation;
using Browser.TopPanel.Wpf.NavigationPanel;
using Browser.TopPanel.Wpf.TabsPanel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.ViewModel;

namespace Browser.TopPanel.Wpf;

internal class TopPanelViewModel : ViewModelBase, IRecipient<BrowserSearchAddressChangedMessage>
{
    public TabsPanelViewModel TabsPanelViewModel { get; }
    
    public  NavigationPanelViewModel NavigationPanelViewModel { get; }
    
    private readonly IBrowserRouter _browserRouter;
    
    private string _searchAddress;
    public string SearchAddress
    {
        get => _searchAddress;
        set => SerProperty(ref _searchAddress, value);
    }
    
    public ICommand SearchCommand { get; }
    

    public TopPanelViewModel(
        IBrowserRouter browserRouter,
        TabsPanelViewModel tabsPanelViewModel,
        NavigationPanelViewModel navigationPanelViewModel)
    {
        TabsPanelViewModel = tabsPanelViewModel;
        NavigationPanelViewModel = navigationPanelViewModel;
        
        _searchAddress = string.Empty;
        _browserRouter = browserRouter;
        
        SearchCommand = new RelayCommand(OnSearch);
    }

    private void OnSearch()
    {
        _browserRouter.Navigate(SearchAddress);
    }
    

    public void Receive(BrowserSearchAddressChangedMessage message)
    {
        SearchAddress = message.Address;
    }
}