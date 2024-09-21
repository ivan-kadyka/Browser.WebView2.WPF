using System;
using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.TopPanel.Wpf.TabsPanel;

public class PageTabItemViewModel : ViewModelBase
{
    public PageId Id => _page.Id;
    
    public string Header
    {
        get
        {
            var header = string.IsNullOrWhiteSpace(_page.Title) ? "New Tab" : _page.Title;

            header = header.TrimStart("www.".ToCharArray());
            
            return header;
        }
    }
    
    private readonly IPage _page;
    
    public PageTabItemViewModel(IPage page)
    {
        _page = page;
        _page.Source.Subscribe(OnPathChanged);
    }

    private void OnPathChanged(Uri source)
    {
        OnPropertyChanged(nameof(Header));
    }
}