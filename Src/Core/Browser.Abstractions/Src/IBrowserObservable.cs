using Browser.Abstractions.Page;
using Reactive.Extensions.Observable;

namespace Browser.Abstractions;

/// <summary>
/// Provides observables for browser-related events, such as page additions, removals, and current page changes. 
/// It also exposes a collection of pages and the current active page as observable values.
/// </summary>
public interface IBrowserObservable
{
    /// <summary>
    /// Gets an observable stream that notifies when a page is added.
    /// </summary>
    IObservable<IPage> PageAdded { get; }

    /// <summary>
    /// Gets an observable stream that notifies when a page is removed.
    /// </summary>
    IObservable<IPage> PageRemoved { get; }

    /// <summary>
    /// Gets an observable stream that notifies when a page is reloaded.
    /// </summary>
    IObservable<IPage> PageReloaded { get; }
    
    /// <summary>
    /// Gets a read-only list of all the pages in the browser.
    /// </summary>
    IReadOnlyList<IPage> Pages { get; }

    /// <summary>
    /// Gets the current active page as an observable value.
    /// </summary>
    IObservableValue<IPage> CurrentPage { get; }
}