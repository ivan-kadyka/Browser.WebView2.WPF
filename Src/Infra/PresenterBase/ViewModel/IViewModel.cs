using System.ComponentModel;

namespace PresenterBase.ViewModel;

/// <summary>
/// Defines the base interface for view models, providing notifications when properties change.
/// It combines both INotifyPropertyChanged and INotifyPropertyChanging to signal before and after property updates.
/// </summary>
public interface IViewModel : INotifyPropertyChanged, INotifyPropertyChanging
{
}