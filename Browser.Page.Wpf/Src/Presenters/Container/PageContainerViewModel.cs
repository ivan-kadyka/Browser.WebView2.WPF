using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Controls;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.Page.Wpf.Presenters.Container;

public class PageContainerViewModel : ViewModelBase
{
    private readonly IBrowser _browser;

    public ObservableCollection<TabItem> WebView2Tabs
    {
        get { return _webView2Tabs; }
        set
        {
            if (_webView2Tabs == value)
                return;

            //set value
            _webView2Tabs = value;

            OnPropertyChanged();
        }
    }
    
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set
        {
            if (_selectedIndex == value)
                return;

            //set value
            _selectedIndex = value;

            OnPropertyChanged();
        }
    }
    
    private int _tabCount = 0;
    private int _selectedIndex = 0;

    private ObservableCollection<TabItem> _webView2Tabs = new ObservableCollection<TabItem>();
    
    private readonly CompositeDisposable _disposables = new();
    
    public PageContainerViewModel(IBrowser browser)
    {
        _browser = browser;
        var currentPage = browser.CurrentPage.Value;
        
        
        AddTab(currentPage);
        
        _disposables.Add(browser.PageAdded.Subscribe(OnPageAdded));
        _disposables.Add(browser.CurrentPage.Subscribe(OnCurrentPageChanged));
    }
    
    private void OnPageAdded(IBrowserPage page)
    {
        AddTab(page);
    }

    private async void OnCurrentPageChanged(IBrowserPage page)
    {
        for (int i = 0; i < _webView2Tabs.Count; i++)
        {
            if (_webView2Tabs[i].Content == page.Content)
            {
                SelectedIndex = i;
                return;
            }
        }
    }


    private async void AddTab(IBrowserPage page)
    {
        _tabCount++;
        
        //add TabItem
        _webView2Tabs.Add(new TabItem { Header = null, Content = page.Content, Name = $"tab_{_tabCount}" });
        
        SelectedIndex = _webView2Tabs.Count - 1;
        
        await page.Load();
    }
}