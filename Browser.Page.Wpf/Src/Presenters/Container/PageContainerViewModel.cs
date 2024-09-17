using System;
using System.Reactive.Disposables;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.Page.Wpf.Presenters.Container;

public class PageContainerViewModel : ViewModelBase
{
    private readonly IBrowser _browser;

    public object WebContent
    {
        get => _content;
        private set
        {
            if (_content == value)
                return;
            
            _content = value;
            OnPropertyChanged();
        }
    }
    
    
    private object _content;
    
    
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

    private void OnCurrentPageChanged(IBrowserPage page)
    {
        WebContent = page.Content;
    }


    private async void AddTab(IBrowserPage page)
    {
        WebContent = page.Content;
        
        await page.Load();
    }
}