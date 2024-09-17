using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.Page.Wpf.Page;

internal class PageViewModel : ViewModelBase
{
    
    public object Content { get; }

    public Uri Source { get; private set; }

    public PageViewModel(IBrowserPage page, string url)
    {
        Content = page.Content;
        Source = new Uri(url);
    }
}