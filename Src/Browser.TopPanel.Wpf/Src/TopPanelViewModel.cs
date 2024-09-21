using Browser.TopPanel.Wpf.NavigationPanel;
using Browser.TopPanel.Wpf.SearchBar;
using Browser.TopPanel.Wpf.TabsPanel;
using PresenterBase.ViewModel;

namespace Browser.TopPanel.Wpf;

internal class TopPanelViewModel : ViewModelBase
{
    public TabsPanelViewModel TabsPanelViewModel { get; }
    
    public  NavigationPanelViewModel NavigationPanelViewModel { get; }
    
    public SearchBarViewModel SearchBarViewModel { get; }
    

    public TopPanelViewModel(
        TabsPanelViewModel tabsPanelViewModel,
        NavigationPanelViewModel navigationPanelViewModel,
        SearchBarViewModel searchBarViewModel)
    {
        TabsPanelViewModel = tabsPanelViewModel;
        NavigationPanelViewModel = navigationPanelViewModel;
        SearchBarViewModel = searchBarViewModel;
    }
}