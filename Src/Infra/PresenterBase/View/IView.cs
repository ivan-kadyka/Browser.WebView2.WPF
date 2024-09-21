namespace PresenterBase.View;

/// <summary>
/// Represents the base interface for views. The view binds to a data context, typically a view model.
/// </summary>
public interface IView
{
    /// <summary>
    /// Gets or sets the data context for this view. This is usually a view model providing data and commands to the view.
    /// </summary>
    object DataContext { get; set; }
}