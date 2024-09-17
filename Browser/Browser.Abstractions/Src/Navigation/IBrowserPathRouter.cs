using Reactive.Extensions.Observable;

namespace Browser.Abstractions.Navigation;

public interface IBrowserPathRouter
{
    IObservableValue<INavigateOptions> Path { get; }
}