using System.Reactive.Disposables;
using Browser.Abstractions;
using Browser.Abstractions.Content;
using Browser.Abstractions.Navigation;
using Browser.Core.Stack;
using Browser.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Reactive.Extensions.Observable;

namespace Browser.Core;

internal class BrowserPage : DisposableBase, IBrowserPage
{
    private readonly IMessenger _messenger;
    public string Id { get; }

    public string Title { get; }
    
    public IContent Content { get; }

    public IObservableValue<INavigateOptions> Path  => _history.Current;

    private readonly UndoRedoStack<INavigateOptions> _history;
    
    private readonly CompositeDisposable _disposables = new();
    
    public BrowserPage(IMessenger messenger, INavigateOptions options)
    {
        Id = Guid.NewGuid().ToString();
        Title = options.Address;
        
        _messenger = messenger;
        _history = new UndoRedoStack<INavigateOptions>(options);
        
        _disposables.Add(_history.Current.Subscribe(it =>
        {
            _messenger.Send(new NavigationPathChangedMessage(it));
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