using Browser.Abstractions.Page;
using Reactive.Extensions.Observable;

namespace Browser.Abstractions;

public interface IBrowserObservable
{
    IObservable<IPage> PageAdded { get; }
    
    IObservable<IPage> PageRemoved { get; }
    
    IReadOnlyList<IPage> Pages { get; }
    
    IObservableValue<IPage> CurrentPage { get; }
}