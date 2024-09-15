using Reactive.Extensions.Observable;

namespace Browser.Core.Navigation;

public interface IBrowserPathRouter
{
    IObservableValue<string> Path { get; }
}