using PresenterBase.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Browser.Core;
using CommunityToolkit.Mvvm.Input;

namespace TopPanel.TabsPanel;

internal class TabsPanelViewModel : ViewModelBase
{
    public ObservableCollection<PageTabItemViewModel> Tabs => _tabs;

    private PageTabItemViewModel _selectedPageTab;
    public PageTabItemViewModel SelectedPageTab
    {
        get => _selectedPageTab;
        set
        {
            _selectedPageTab = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddTabCommand { get; }
    public ICommand CloseTabCommand { get; }
    
    private readonly ObservableCollection<PageTabItemViewModel> _tabs;

    public TabsPanelViewModel(IBrowserObservable browserObservable)
    {
        _tabs = new ObservableCollection<PageTabItemViewModel>(
            browserObservable.Pages.Select(it => new PageTabItemViewModel(it)));
       
        SelectedPageTab = Tabs[0];

        AddTabCommand = new RelayCommand(AddTab);
        CloseTabCommand = new RelayCommand<PageTabItemViewModel>(CloseTab);
    }

    private void AddTab()
    {
        //  var newTab = new TabItemViewModel("New Tab", "");
        //Tabs.Add(newTab);
       // SelectedTab = newTab;
    }

    private void CloseTab(PageTabItemViewModel? tabItem)
    {
        if (tabItem != null && Tabs.Contains(tabItem))
        {
            Tabs.Remove(tabItem);
        }
    }
}