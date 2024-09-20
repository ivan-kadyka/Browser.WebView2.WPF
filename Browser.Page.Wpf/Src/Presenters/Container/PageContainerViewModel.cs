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
    private readonly IBrowser _browser;
    
    public PageContainerViewModel(IBrowser browser)
    {
        _browser = browser;
        _content =  browser.CurrentPage.Value;
        _disposables.Add(browser.CurrentPage.Subscribe(OnCurrentPageChanged));
    }

    private async void OnCurrentPageChanged(IPage page)
    {
        WebContent = page.Content;
        await _browser.LoadPage(page.Id);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}