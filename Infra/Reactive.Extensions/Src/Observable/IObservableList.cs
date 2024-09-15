
namespace Reactive.Extensions.Observable;

public interface IObservableList<out T> : IObservableValue<IReadOnlyList<T>>
{
    
}

public class ObservableList<T> : ObservableValue<IReadOnlyList<T>>, IObservableList<T>
{
    public ObservableList(IReadOnlyList<T> defaultValue) : base(defaultValue)
    {
    }
}