using Browser.Core;
using Browser.Core.Navigation;
using Browser.Stack;
using Reactive.Extensions.Observable;

namespace Browser;

internal class BrowserPage : IBrowserPage
{
    public string Id { get; }

    public string Title { get; }
    
    public IObservableValue<string> Path  => _history.Current;

    private readonly UndoRedoStack<string> _history;
    
    public BrowserPage()
    {
        Id = Guid.NewGuid().ToString();
        _history = new("duckduckgo.com");
        Title = "Browser Page";
    }

    public void Forward()
    {
        _history.Redo();
    }

    public bool CanForward => _history.CanRedo;

    public void Back()
    {
        _history.Undo();
    }

    public bool CanBack => _history.CanUndo;

    public void Refresh()
    {
    }

    public bool CanRefresh => !string.IsNullOrWhiteSpace(_history.Current.Value);

    public void Push(INavigateOptions options)
    {
        _history.Do(options.Address);
    }

    public void Replace(INavigateOptions options)
    {
       // _history.Redo(options.Address);
    }
}