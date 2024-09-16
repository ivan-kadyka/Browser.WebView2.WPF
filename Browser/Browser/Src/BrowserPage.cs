using System.Reactive.Disposables;
using Browser.Core;
using Browser.Core.Navigation;
using Browser.Messages;
using Browser.Stack;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Reactive.Extensions.Observable;

namespace Browser;

internal class BrowserPage : DisposableBase, IBrowserPage
{
    private readonly IMessenger _messenger;
    public string Id { get; }

    public string Title { get; }
    
    public IObservableValue<INavigateOptions> Path  => _history.Current;

    private readonly UndoRedoStack<INavigateOptions> _history;
    
    private readonly CompositeDisposable _disposables = new();
    
    public BrowserPage(IMessenger messenger)
    {
        Id = Guid.NewGuid().ToString();
        Title = "Browser Page";
        
        _messenger = messenger;
        _history = new(new UrlNavigateOptions("duckduckgo.com"));
        
        _disposables.Add(_history.Current.Subscribe(options =>
        {
            _messenger.Send(new NavigationPathChangedMessage(options));
        }));
    }

    public void Forward()
    {
        if (_history.CanRedo)
        {
            _history.Redo();
            _messenger.Send(new BrowserForwardMessage());
        }
    }

    public bool CanForward => _history.CanRedo;

    public void Back()
    {
        _history.Undo();
        _messenger.Send(new BrowserBackMessage());
    }

    public bool CanBack => _history.CanUndo;

    public void Refresh()
    {
    }

    public bool CanRefresh => !string.IsNullOrWhiteSpace(_history.Current.Value.Address);

    public void Push(INavigateOptions options)
    {
        _history.Do(options);
    }

    public void Replace(INavigateOptions options)
    {
       // _history.Redo(options.Address);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables.Dispose();
        }
    }
}