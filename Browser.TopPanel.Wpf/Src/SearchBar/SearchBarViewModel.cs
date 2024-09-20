using System.Windows.Input;
using Browser.Abstractions.Navigation;
using Browser.Messenger;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Browser.TopPanel.Wpf.SearchBar;

internal class SearchBarViewModel : ObservableRecipient, IRecipient<BrowserSearchAddressChangedMessage>
{
    public ICommand SearchCommand { get; }

    public string SearchAddress
    {
        get => _searchAddress;
        set => SetProperty(ref _searchAddress, value);
    }
    
    private readonly INavigationRouter _navigationRouter;
    private string _searchAddress;
    
    
    public SearchBarViewModel(INavigationRouter navigationRouter, IMessenger messenger)
        : base(messenger)
    {
        _searchAddress = string.Empty;
        _navigationRouter = navigationRouter;
        
        SearchCommand = new RelayCommand(OnSearch);
    }
    
    private void OnSearch()
    {
        _navigationRouter.Navigate(SearchAddress);
    }
    

    public void Receive(BrowserSearchAddressChangedMessage message)
    {
        SearchAddress = message.Address;
    }
}