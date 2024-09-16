using System.Reactive.Subjects;
using Browser.Core;
using Browser.Core.Navigation;
using Disposable;
using Reactive.Extensions;
using Reactive.Extensions.Observable;

namespace Browser;

public class Browser : DisposableBase, IBrowser
{
    public IObservableValue<IBrowserPage> CurrentPage => _currentPageSubject;

    public bool CanForward => ActivePage.CanForward;
    
    public bool CanBack => ActivePage.CanBack;
    
    public IObservableValue<string> Path => ActivePage.Path;

    public IObservable<IBrowserPage> PageAdded => _pageAdded;
    public IObservable<IBrowserPage> PageRemoved => _pageRemoved;
    public IObservableList<IBrowserPage> Pages {get;}
    
    private readonly Subject<IBrowserPage> _pageAdded = new();
    private readonly Subject<IBrowserPage> _pageRemoved = new();
    private readonly ObservableList<IBrowserPage> _pages = new(new List<IBrowserPage>());

    private readonly ObservableValue<IBrowserPage> _currentPageSubject;
    private IBrowserPage ActivePage => _currentPageSubject.Value;
    
    public Browser()
    {
        Pages = _pages;
        _currentPageSubject = new ObservableValue<IBrowserPage>(new BrowserPage());
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
       _pageAdded.OnNext(page);

       var newPages = _pages.Value.ToList();
       newPages.Add(page);
       
       _pages.OnNext(newPages);
       SetCurrentPage(page);
       
       return Task.CompletedTask;
    }

    public Task RemovePage(IBrowserPage page)
    {
        _pageRemoved.OnNext(page);
        
        var newPages = _pages.Value.ToList();
        newPages.Remove(page);
        
        _pages.OnNext(newPages);
        
        if (newPages.Count > 0)
        {
            SetCurrentPage(newPages.Last());
        }
        
        return Task.CompletedTask;
    }

    public void SetCurrentPage(IBrowserPage page)
    {
        _currentPageSubject.OnNext(page);
    }

    protected override void Dispose(bool disposing)
    {
        
    }
}