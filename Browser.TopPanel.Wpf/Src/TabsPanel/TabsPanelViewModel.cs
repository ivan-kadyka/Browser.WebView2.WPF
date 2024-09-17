using System;
using PresenterBase.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using Browser.Core.Commands;
using CommunityToolkit.Mvvm.Input;

namespace Browser.TopPanel.Wpf.TabsPanel;

internal class TabsPanelViewModel : ViewModelBase
{
    private readonly RemoveBrowserPageCommand _removeBrowserPageCommand;
    private readonly SelectBrowserPageCommand _selectBrowserPageCommand;
    public ObservableCollection<PageTabItemViewModel> Tabs => _tabs;

    private PageTabItemViewModel _selectedPageTab;
    public PageTabItemViewModel SelectedPageTab
    {
        get => _selectedPageTab;
        set
        {
            if (value == _selectedPageTab)
                return;
            
            _selectedPageTab = value;
            _selectBrowserPageCommand.Execute(value.Id);
            OnPropertyChanged();
        }
    }

    public ICommand AddTabCommand { get; }
    public ICommand CloseTabCommand { get; }
    
    private readonly ObservableCollection<PageTabItemViewModel> _tabs;

    public TabsPanelViewModel(IBrowserObservable browserObservable,
        AddBrowserPageCommand addBrowserPageCommand,
        RemoveBrowserPageCommand removeBrowserPageCommand,
        SelectBrowserPageCommand selectBrowserPageCommand)
    {
        _removeBrowserPageCommand = removeBrowserPageCommand;
        _selectBrowserPageCommand = selectBrowserPageCommand;

        _tabs = new ObservableCollection<PageTabItemViewModel>(
            browserObservable.Pages.Select(it => new PageTabItemViewModel(it)));
        
        browserObservable.PageAdded.Subscribe(OnAddNewPage);
        browserObservable.PageRemoved.Subscribe(OnRemovePage);
        browserObservable.CurrentPage.Subscribe(OnActivePageChanged);
        
        AddTabCommand = addBrowserPageCommand;
        CloseTabCommand = new RelayCommand<PageTabItemViewModel>(CloseTab);
        
        if (_tabs.Count > 0)
            _selectedPageTab = Tabs[0];
    }

    private void OnAddNewPage(IBrowserPage page)
    {
        var tabItem = new PageTabItemViewModel(page);
        Tabs.Add(tabItem);
    }
    
    private void OnRemovePage(IBrowserPage page)
    {
        var tabItem = Tabs.FirstOrDefault(it => it.Id == page.Id);

        if (tabItem != null)
        {
            Tabs.Remove(tabItem);
        }
    }
    
    private void OnActivePageChanged(IBrowserPage? page)
    {
        if (page == null)
            return;
        
        var tabItem = Tabs.FirstOrDefault(it => it.Id == page.Id);

        if (tabItem != null)
        {
            SelectedPageTab = tabItem;
        }
    }
    
    private void CloseTab(PageTabItemViewModel? tabItem)
    {
        if (tabItem != null)
        {
            _removeBrowserPageCommand.Execute(tabItem.Id);
        }
    }
}