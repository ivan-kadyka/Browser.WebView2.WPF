using Browser.Core;
using Browser.Core.Navigation;
using Reactive.Extensions;
using Reactive.Extensions.Observable;

namespace Browser;

public class Browser : IBrowser
{
    public IObservableValue<IBrowserPage> CurrentPage { get; }
    
    public bool CanForward => _currentPage.CanForward;
    
    public bool CanBack => _currentPage.CanBack;
    
    public IObservableValue<string> Path => _currentPage.Path;

    
    private IBrowserPage _currentPage;
    
    public Browser()
    {
        _currentPage = new BrowserPage();
        CurrentPage = new ObservableValue<IBrowserPage>(_currentPage);
    }
    
    public void Forward()
    {
        _currentPage.Forward();
    }

    public void Back()
    {
        _currentPage.Back();
    }

    public void Refresh()
    {
        _currentPage.Refresh();
    }

    public void Push(INavigateOptions options)
    {
        _currentPage.Push(options);
    }

    public void Replace(INavigateOptions options)
    {
        _currentPage.Replace(options);
    }
}