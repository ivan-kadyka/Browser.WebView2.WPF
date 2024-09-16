﻿using System;
using Browser.Core;
using PresenterBase.ViewModel;

namespace TopPanel.TabsPanel;

public class PageTabItemViewModel : ViewModelBase
{
    public string Header
    {
        get
        {
            var header = string.IsNullOrWhiteSpace(_page.Title) ? "New Tab" : _page.Title;
            return header;
        }
    }
    
    private readonly IBrowserPage _page;
    
    public PageTabItemViewModel(IBrowserPage page)
    {
        _page = page;
        _page.Path.Subscribe(new Action<object>(it => { OnPropertyChanged(); }));
    }

    private void OnPropertyChanged()
    {
        OnPropertyChanged(nameof(Header));
    }
}