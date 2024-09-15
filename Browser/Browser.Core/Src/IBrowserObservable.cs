using Reactive.Extensions.Observable;

namespace Browser.Core;

public interface IBrowserObservable
{
    IObservable<IBrowserPage> PageAdded { get; }
    
    IObservable<IBrowserPage> PageRemoved { get; }
    
    IObservableList<IBrowserPage> Pages { get; }
    
    IObservableValue<IBrowserPage> CurrentPage { get; }
}