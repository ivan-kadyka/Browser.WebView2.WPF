using Browser.Core;
using Browser.Core.Navigation;
using Reactive.Extensions;
using Reactive.Extensions.Observable;

namespace Browser;

internal class BrowserPage : IBrowserPage
{
    public IObservableValue<string> Path  => _pathSubject;
    
    private readonly ObservableValue<string> _pathSubject = new("duckduckgo.com");
    
    
    public void Forward()
    {
    }

    public bool CanForward { get; }

    public void Back()
    {
    }

    public bool CanBack { get; }

    public void Refresh()
    {
    }

    public void Push(INavigateOptions options)
    {
        _pathSubject.OnNext(options.Address);
    }

    public void Replace(INavigateOptions options)
    {
        _pathSubject.OnNext(options.Address);
    }
}