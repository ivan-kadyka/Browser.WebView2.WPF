using Reactive.Extensions.Observable;

namespace Browser.Abstractions.Navigation;

public interface IBrowserPathRouter
{
    IObservableValue<Uri> Path { get; }
}