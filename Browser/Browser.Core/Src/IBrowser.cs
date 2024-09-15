using Browser.Core.Navigation;
using Reactive.Extensions.Observable;

namespace Browser.Core;

public interface IBrowser : IBrowserRouter
{
    IObservableValue<IBrowserPage> CurrentPage { get; }
    
    
}