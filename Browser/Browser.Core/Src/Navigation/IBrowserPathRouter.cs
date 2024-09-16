using Reactive.Extensions.Observable;

namespace Browser.Core.Navigation;

public interface IBrowserPathRouter
{
    IObservableValue<INavigateOptions> Path { get; }
}