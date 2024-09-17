using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using Disposable;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Presenters.Container;

public class PageContainerPresenter : DisposableBase, IPresenter, INotifyPropertyChanged
{
    public object Content
    {
        get
        {
            return _content;
        }
        private set
        {
            _content = value;
            OnPropertyChanged();
        }
    }

    private readonly IBrowser _browser;
    private IPresenter _currentPagePresenter;
    
    private readonly CompositeDisposable _disposables = new();
    
    private readonly Dictionary<string, IPresenter> _presenters = new();
    private object _content;
    
    public PageContainerPresenter(IBrowser browser)
    {
        _browser = browser;

        var currentPage = browser.CurrentPage.Value;
        
        _currentPagePresenter = CreatePagePresenter(currentPage);
        Content = _currentPagePresenter.Content;
        
        _disposables.Add(_currentPagePresenter);
        _disposables.Add(browser.PageAdded.Subscribe(OnPageAdded));
        _disposables.Add(browser.PageRemoved.Subscribe(OnPageRemoved));
        
        _disposables.Add(browser.CurrentPage.Subscribe(OnCurrentPageChanged));
    }
    

    private void OnPageAdded(IBrowserPage page)
    {
        var pagePresenter = CreatePagePresenter(page);
        _presenters[page.Id] = pagePresenter;
    }

    private void OnPageRemoved(IBrowserPage page)
    {
        if (_presenters.Remove(page.Id, out var presenter))
        {
            presenter.Dispose();
        }
    }
    
    private async void OnCurrentPageChanged(IBrowserPage page)
    {
        if (_presenters.TryGetValue(page.Id, out var presenter))
        {
            if (_currentPagePresenter == presenter)
                return;
            
            await _currentPagePresenter.Stop();
            
            _currentPagePresenter = presenter;
            OnPropertyChanged(nameof(Content));
            
           // await Task.Yield();
            
            await presenter.Start();
        }
    }
   
    public async Task Start(CancellationToken token = default)
    {
        await _currentPagePresenter.Start(token);
    }

    public async Task Stop(CancellationToken token = default)
    {
        await _currentPagePresenter.Stop(token);
    }

    private IPresenter CreatePagePresenter(IBrowserPage page)
    {
        var presenter = new PagePresenter(page);
        _presenters[page.Id] = presenter;
        
        return presenter;
    }
    
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        
        _disposables.Dispose();
           
        foreach (var presenter in _presenters.Values)
            presenter.Dispose();
           
        _presenters.Clear();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}

