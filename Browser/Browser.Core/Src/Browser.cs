using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Messenger;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Reactive.Extensions.Observable;

namespace Browser.Core;

public class Browser : DisposableBase, IBrowser
{
    private readonly IMessenger _messenger;
    public IObservableValue<IPage> CurrentPage => _currentPageSubject;

    public bool CanForward => ActivePage.CanForward;
    
    public bool CanBack => ActivePage.CanBack;

    public IObservable<IPage> PageAdded => _pageAdded;
    public IObservable<IPage> PageRemoved => _pageRemoved;
    public IReadOnlyList<IPage> Pages => _pages;
    
    private readonly Subject<IBrowserPage> _pageAdded = new();
    private readonly Subject<IBrowserPage> _pageRemoved = new();
    
    private readonly List<IBrowserPage> _pages = new();

    private readonly ObservableValue<IBrowserPage> _currentPageSubject;
    private IBrowserPage ActivePage => _currentPageSubject.Value;
    
    public IObservableValue<Uri> Path => _pathObservable;
    private readonly ObservableValue<Uri> _pathObservable;
    
    private readonly CompositeDisposable _disposables = new();
    
    public Browser(IMessenger messenger, IBrowserPageFactory browserPageFactory)
    {
        _messenger = messenger;
        
        var navigationOptions = new UrlNavigateOptions("duckduckgo.com");
        var page = browserPageFactory.Create(navigationOptions);
        
        _pages.Add(page);

        _pathObservable = new ObservableValue<Uri>(new Uri(navigationOptions.Address));
        _currentPageSubject = new ObservableValue<IBrowserPage>(page);

        SubscribeEvents();    
    }

    private void SubscribeEvents()
    {
        _disposables.Add(_currentPageSubject
            .Select(browserPage => browserPage.Path)
            .Switch()                  
            .Subscribe(uri => _pathObservable.OnNext(uri)));
        
        _disposables.Add(_pathObservable.Subscribe(it =>
        {
            _messenger.Send(new BrowserSearchAddressChangedMessage(it.ToString()));
        }));
        
        _disposables.Add(_currentPageSubject.Subscribe(it =>
        {
            _messenger.Send(new BrowserActivePageChangedMessage(it.Id.ToString()));
        }));
    }
    
    public void Forward()
    {
        ActivePage.Forward();
    }

    public void Back()
    {
        ActivePage.Back();
    }

    public void Refresh()
    {
        ActivePage.Refresh();
    }

    public bool CanRefresh => ActivePage.CanRefresh;

    public void Push(INavigateOptions options)
    {
        ActivePage.Push(options);
    }

    public Task AddPage(IBrowserPage page)
    {
       _pages.Add(page);
       _pageAdded.OnNext(page);
       SetCurrentPage(page);
       
       return Task.CompletedTask;
    }
    
    public Task RemovePage(PageId pageId)
    {
        var page = _pages.FirstOrDefault(it => it.Id == pageId);
        
        return page != null ? RemovePage(page) : Task.CompletedTask;
    }

    public async Task LoadPage(PageId? pageId = default, CancellationToken token = default)
    {
        var page = _pages.FirstOrDefault(it => it.Id == pageId);
        
        if (page != null)
        {
            await page.Load(token);
        }
    }

    private Task RemovePage(IBrowserPage page)
    {
        var isRemoved = _pages.Remove(page);

        if (isRemoved)
        {
            _pageRemoved.OnNext(page);
            page.Dispose();
            
            var lastPage = _pages.LastOrDefault();
            
            if (lastPage != null)
            {
                SetCurrentPage(lastPage);
            }
        }
        
        return Task.CompletedTask;
    }

    public async Task ReloadPage(PageId? pageId = default, CancellationToken token = default)
    {
        if (pageId == null)
        {
            pageId = CurrentPage.Value.Id;
        }
        
        var page = _pages.FirstOrDefault(it => it.Id == pageId);
        
        if (page != null)
        {
          await ReloadPage(page, token);
        }
    }
    
    private async Task ReloadPage(IBrowserPage page, CancellationToken token = default)
    {
        try
        {
            await page.Reload(token);
        }
        catch (OperationCanceledException)
        {

        }
        catch (Exception)
        {
            //  logger.LogError(ex, "Reload page failed");
        }
    }
    
    public void SetCurrentPage(PageId pageId)
    {
        var page = _pages.FirstOrDefault(it => it.Id == pageId);
        
        if (page != null)
        {
            SetCurrentPage(page);
        }
    }

    private void SetCurrentPage(IBrowserPage page)
    {
        _currentPageSubject.OnNext(page);
    }
    

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _pageAdded.Dispose();
            _pageRemoved.Dispose();
            
            _disposables.Dispose();
        }
    }
}