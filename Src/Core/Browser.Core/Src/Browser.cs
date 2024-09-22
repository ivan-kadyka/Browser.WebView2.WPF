using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Browser.Abstractions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Page.Factory;
using Browser.Messenger;
using Browser.Settings.Abstractions;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Microsoft.Extensions.Logging;
using Reactive.Extensions.Observable;

namespace Browser.Core;

public class Browser : DisposableBase, IBrowser
{
    private readonly IMessenger _messenger;
    private readonly IBrowserPageFactory _browserPageFactory;
    private readonly IBrowserSettings _settings;
    private readonly ILogger<IBrowser> _logger;
    public IObservableValue<IPage> CurrentPage => _currentPageSubject;

    public bool CanForward => ActivePage.CanForward;
    
    public bool CanBack => ActivePage.CanBack;

    public IObservable<IPage> PageAdded => _pageAdded;
    public IObservable<IPage> PageRemoved => _pageRemoved;
    public IObservable<IPage> PageReloaded => _pageReloaded;
    public IReadOnlyList<IPage> Pages => _pages;
    
    private readonly Subject<IBrowserPage> _pageAdded = new();
    private readonly Subject<IBrowserPage> _pageRemoved = new();
    private readonly Subject<IBrowserPage> _pageReloaded = new();
    
    private readonly List<IBrowserPage> _pages = new();

    private readonly ObservableValue<IBrowserPage> _currentPageSubject;
    private IBrowserPage ActivePage => _currentPageSubject.Value;
    
    public IObservableValue<Uri> Source => _sourceObservable;
    private readonly ObservableValue<Uri> _sourceObservable;
    
    private readonly CompositeDisposable _disposables = new();
    
    public Browser(
        IMessenger messenger,
        IBrowserPageFactory browserPageFactory,
        IBrowserSettings settings,
        ILogger<IBrowser> logger)
    {
        _messenger = messenger;
        _browserPageFactory = browserPageFactory;
        _settings = settings;
        _logger = logger;

        var homePage = CreateHomePage();

        _sourceObservable = new ObservableValue<Uri>(homePage.Source.Value);
        _currentPageSubject = new ObservableValue<IBrowserPage>(homePage);

        SubscribeEvents();    
    }

    private void SubscribeEvents()
    {
        _disposables.Add(_currentPageSubject
            .Select(browserPage => browserPage.Source)
            .Switch()                  
            .Subscribe(uri => _sourceObservable.OnNext(uri)));
        
        _disposables.Add(_sourceObservable.Subscribe(it =>
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

    public void Reload()
    {
        ActivePage.Reload();
    }

    public bool CanReload => ActivePage.CanReload;

    public void Push(INavigateOptions options)
    {
        ActivePage.Push(options);
    }

    public Task<IPage> CreatePage(IPageCreateOptions? options, CancellationToken token = default)
    {
        if (options == null)
            options = GetDefaultPageCreateOptions();
        
        var page = CreatePageInternal(options);
        
        return Task.FromResult<IPage>(page);
    }
    
    private IBrowserPage CreatePageInternal(IPageCreateOptions options)
    {
        var page = _browserPageFactory.Create(options);
        
        _pages.Add(page);
        _pageAdded.OnNext(page);
        
        if (options.SetActive)
            SetCurrentPage(page);
        
        return page;
    }
    
    private IPageCreateOptions GetDefaultPageCreateOptions()
    {
        var sourceUri = new Uri("about:blank"); 
        return new PageCreateOptions(sourceUri);
    }

    private IBrowserPage CreateHomePage()
    {
        var source = _settings.General.HomeAddress;
        var sourceUri = new Uri(source);
        var createOptions = new PageCreateOptions(sourceUri);
        
        var page = _browserPageFactory.Create(createOptions);
        _pages.Add(page);
        
        return page;
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
            page.Dispose();
            _pageRemoved.OnNext(page);
            
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
            _pageReloaded.OnNext(page);
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Reload page failed, pageId: {page.Id}, pageTitle: {page.Source.Value}");
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