using PresenterBase.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TopPanel.TabsPanel;

public class TabsPanelViewModel : ViewModelBase
{
    public ObservableCollection<TabItemViewModel> Tabs { get; set; }

    private TabItemViewModel _selectedTab;
    public TabItemViewModel SelectedTab
    {
        get => _selectedTab;
        set
        {
            _selectedTab = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddTabCommand { get; }
    public ICommand CloseTabCommand { get; }

    public TabsPanelViewModel()
    {
        Tabs = new ObservableCollection<TabItemViewModel>
        {
            new TabItemViewModel { Header = "Tab 1" },
            new TabItemViewModel { Header = "Tab 2"},
            new TabItemViewModel { Header = "Tab 3" }
        };
        SelectedTab = Tabs[0];

        AddTabCommand = new RelayCommand(AddTab);
        CloseTabCommand = new RelayCommand(CloseTab);
    }

    private void AddTab(object obj)
    {
        var newTab = new TabItemViewModel { Header = $"Tab {Tabs.Count + 1}"};
        Tabs.Add(newTab);
        SelectedTab = newTab;
    }

    private void CloseTab(object tabItem)
    {
        if (tabItem is TabItemViewModel tab && Tabs.Contains(tab))
        {
            Tabs.Remove(tab);
        }
    }
}

public class TabItemViewModel
{
    public string Header { get; set; }
}
