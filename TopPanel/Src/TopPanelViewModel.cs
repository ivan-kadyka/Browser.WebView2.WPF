using System.Windows.Input;
using Browser.Core.Navigation;
using PresenterBase.ViewModel;
using TopPanel.Command;

namespace TopPanel;

internal class TopPanelViewModel : ViewModelBase
{
    private readonly IBrowserRouter _browserRouter;
    public string Title { get; set; } = "TopPanel Title";
    
    private string _searchAddress;
    public string SearchAddress
    {
        get => _searchAddress;
        set => SetField(ref _searchAddress, value);
    }

    // Define the command
    public ICommand SearchCommand { get; }

    public TopPanelViewModel(IBrowserRouter browserRouter)
    {
        _browserRouter = browserRouter;
        SearchCommand = new RelayCommand(OnSearch);
    }

    private void OnSearch()
    {
        _browserRouter.Navigate(SearchAddress);
    }
}