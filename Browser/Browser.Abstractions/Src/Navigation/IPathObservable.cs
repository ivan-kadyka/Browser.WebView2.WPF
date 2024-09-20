using Reactive.Extensions.Observable;

namespace Browser.Abstractions.Navigation;

public interface IPathObservable
{
    IObservableValue<Uri> Source { get; }
}