using System.Windows.Input;
using Browser.Core.Navigation;
using CommunityToolkit.Mvvm.Input;
using PresenterBase.ViewModel;
using TopPanel.TabsPanel;

namespace TopPanel;

internal class TopPanelViewModel : ViewModelBase
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

    public TopPanelViewModel(IBrowserRouter browserRouter)
    {
        _searchAddress = string.Empty;
        _browserRouter = browserRouter;
        SearchCommand = new RelayCommand(OnSearch);
        TabsPanelViewModel = new TabsPanelViewModel();
    }

    private void OnSearch()
    {
        _browserRouter.Navigate(SearchAddress);
    }
}