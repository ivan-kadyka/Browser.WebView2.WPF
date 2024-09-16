using System.Reactive.Subjects;
using Browser.Core;
using Browser.Core.Navigation;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Reactive.Extensions;
using Reactive.Extensions.Observable;

namespace Browser;

public class Browser : DisposableBase, IBrowser
{
    public IObservableValue<IBrowserPage> CurrentPage => _currentPageSubject;

    public bool CanForward => ActivePage.CanForward;
    
    public bool CanBack => ActivePage.CanBack;
    
    public IObservableValue<INavigateOptions> Path => ActivePage.Path;

    public IObservable<IBrowserPage> PageAdded => _pageAdded;
    public IObservable<IBrowserPage> PageRemoved => _pageRemoved;
    public IReadOnlyList<IBrowserPage> Pages => _pages;
    
    private readonly Subject<IBrowserPage> _pageAdded = new();
    private readonly Subject<IBrowserPage> _pageRemoved = new();
    
    
    private readonly List<IBrowserPage> _pages = new();

    private readonly ObservableValue<IBrowserPage> _currentPageSubject;
    private IBrowserPage ActivePage => _currentPageSubject.Value;
    
    public Browser(IMessenger messenger)
    {
        var navigationOptions = new UrlNavigateOptions("duckduckgo.com");
        var page = new BrowserPage(messenger, navigationOptions);
        _pages.Add(page);
        
        _currentPageSubject = new ObservableValue<IBrowserPage>(page);
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

    public void Replace(INavigateOptions options)
    {
        ActivePage.Replace(options);
    }

    public Task AddPage(IBrowserPage page)
    {
       _pages.Add(page);
       _pageAdded.OnNext(page);
       SetCurrentPage(page);
       
       return Task.CompletedTask;
    }

    public Task RemovePage(IBrowserPage page)
    {
        var isRemoved = _pages.Remove(page);

        if (isRemoved)
        {
            _pageRemoved.OnNext(page);
            
            var lastPage = _pages.LastOrDefault();
            
            if (lastPage != null)
            {
                SetCurrentPage(lastPage);
            }
        }
        
        return Task.CompletedTask;
    }

    public void SetCurrentPage(IBrowserPage page)
    {
        _currentPageSubject.OnNext(page);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _pageAdded.Dispose();
            _pageRemoved.Dispose();
        }
    }
}