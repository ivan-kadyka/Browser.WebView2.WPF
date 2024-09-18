using System;
using System.Reactive.Disposables;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.Page.Wpf.Presenters.Container;

public class PageContainerViewModel : ViewModelBase, IDisposable
{
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
        _content =  browser.CurrentPage.Value;
        _disposables.Add(browser.CurrentPage.Subscribe(OnCurrentPageChanged));
    }
    

    private async void OnCurrentPageChanged(IBrowserPage page)
    {
        WebContent = page.Content;
        await page.Load();
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}