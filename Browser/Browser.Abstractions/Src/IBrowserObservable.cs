using Reactive.Extensions.Observable;

namespace Browser.Abstractions;

public interface IBrowserObservable
{
    IObservable<IBrowserPage> PageAdded { get; }
    
    IObservable<IBrowserPage> PageRemoved { get; }
    
    IReadOnlyList<IBrowserPage> Pages { get; }
    
    IObservableValue<IBrowserPage> CurrentPage { get; }
}