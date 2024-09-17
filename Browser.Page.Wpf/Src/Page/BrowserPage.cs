using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Messenger;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Reactive.Extensions.Observable;

namespace Browser.Page.Wpf.Page;

internal class BrowserPage : DisposableBase, IBrowserPage
{
    private readonly IWebView2 _webView;
    private readonly IMessenger _messenger;
    public string Id { get; }

    public string Title { get; }
    
    public object Content => _webView;

    public IObservableValue<INavigateOptions> Path  => _history.Current;

    private readonly UndoRedoStack<INavigateOptions> _history;
    
    private readonly CompositeDisposable _disposables = new();
    
    public BrowserPage(
        IWebView2 webView,
        IMessenger messenger, INavigateOptions options)
    {
        Id = Guid.NewGuid().ToString();
        Title = options.Address;

        _webView = webView;
        _messenger = messenger;
        _history = new UndoRedoStack<INavigateOptions>(options);
        
        _disposables.Add(_history.Current.Subscribe(it =>
        {
            _messenger.Send(new NavigationPathChangedMessage(it));
        }));
        
        _disposables.Add(_webView);
        
        _webView.CoreWebView2InitializationCompleted += WebViewOnCoreWebView2InitializationCompleted;
    }

    private void WebViewOnCoreWebView2InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        _webView.CoreWebView2.NewWindowRequested += CoreWebView2OnNewWindowRequested;
    }

    private void CoreWebView2OnNewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
    {
        
    }

    public async Task Load(CancellationToken token = default)
    {
        _webView.Source = new Uri(Path.Value.Address);
        
        await _webView.EnsureCoreWebView2Async();
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
        _webView.CoreWebView2.Navigate(options.Address);
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
            _webView.CoreWebView2InitializationCompleted -= WebViewOnCoreWebView2InitializationCompleted;
            _webView.CoreWebView2.NewWindowRequested -= CoreWebView2OnNewWindowRequested;
        }
    }
}