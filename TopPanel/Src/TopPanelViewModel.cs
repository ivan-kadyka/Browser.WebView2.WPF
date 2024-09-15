using System.Windows.Input;
using Browser.Core.Navigation;
using PresenterBase.ViewModel;
using TopPanel.Command;

namespace TopPanel;

internal class TopPanelViewModel : ViewModelBase
{
    private readonly IBrowserRouter _browserRouter;
    
    private string _searchAddress;
    public string SearchAddress
    {
        get => _searchAddress;
        set => SerProperty(ref _searchAddress, value);
    }
    
    public ICommand SearchCommand { get; }

    public TopPanelViewModel(IBrowserRouter browserRouter)
    {
        _searchAddress = string.Empty;
        _browserRouter = browserRouter;
        SearchCommand = new RelayCommand(OnSearch);
    }

    private void OnSearch()
    {
        _browserRouter.Navigate(SearchAddress);
    }
}