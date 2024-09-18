using System.Windows.Input;
using Browser.Abstractions.Navigation;
using Browser.Messenger;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.ViewModel;

namespace Browser.TopPanel.Wpf.SearchBar;

internal class SearchBarViewModel : ViewModelBase, IRecipient<BrowserSearchAddressChangedMessage>
{
    public ICommand SearchCommand { get; }

    public string SearchAddress
    {
        get => _searchAddress;
        set => SerProperty(ref _searchAddress, value);
    }
    
    private readonly IBrowserRouter _browserRouter;
    private string _searchAddress;
    
    
    public SearchBarViewModel(IBrowserRouter browserRouter)
    {
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