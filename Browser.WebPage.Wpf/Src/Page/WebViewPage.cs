using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Settings;
using Browser.Messenger;
using Browser.Messenger.Navigation;
using Browser.WebPage.Wpf.Utils;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Reactive.Extensions.Observable;

namespace Browser.WebPage.Wpf.Page;

internal class WebViewPage : DisposableBase, IBrowserPage
{
    public PageId Id { get; }

    public string Title => _webView.Source.Host;

    public object Content => _webView;
    
    public bool CanForward => _webView.CanGoForward;

    public bool CanBack => _webView.CanGoBack;
    
    public bool CanReload => _webView.Source != null && !string.IsNullOrWhiteSpace(_webView.Source.Host);

    public IObservableValue<Uri> Source  => _uriSource;
    
    private readonly IWebView2 _webView;
    private readonly IMessenger _messenger;
    private readonly IBrowserPageSettings _settings;
    private readonly ILogger _logger;
    
    private readonly CompositeDisposable _disposables = new();
    private readonly ObservableValue<Uri> _uriSource;
    
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    public WebViewPage(
        PageId id,
        IWebView2 webView,
        IMessenger messenger,
        IBrowserPageSettings settings,
        ILogger logger)
    {
        Id = id;
        
        _webView = webView;
        _messenger = messenger;
        _settings = settings;
        _logger = logger;

        _uriSource = new ObservableValue<Uri>(_settings.Source);
        _webView.Source = _uriSource.Value;
        
        _disposables.Add(_webView);
        _webView.SourceChanged += WebViewOnSourceChanged;
        
        _webView.NavigationStarting += OnNavigationStarting;
        _webView.NavigationCompleted += OnNavigationCompleted;
        
        _logger.LogInformation($"Page created, source: {_settings.Source}");
    }

    private void OnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        _messenger.Send(new NavigationStartingMessage());
    }
    
    private void OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        _logger.LogTrace($"Navigation completed, navigationId: {e.NavigationId}, source: {_webView.Source}, isSuccess: {e.IsSuccess}");
        _messenger.Send(new NavigationCompletedMessage(e.IsSuccess));
    }

    private void WebViewOnSourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        var uri = _webView.Source;
        _uriSource.OnNext(uri);
    }

    public async Task Load(CancellationToken token = default)
    {
        await _webView.EnsureCoreWebView2Async();
    }

    public Task Reload(CancellationToken token)
    { 
        _webView.Reload();
        
       return Task.CompletedTask;
    }

    public void Forward()
    {
        if (_webView.CanGoForward)
        {
            _webView.GoForward();
            _messenger.Send(new BrowserForwardMessage());
        }
    }
    
    public void Back()
    {
        if (_webView.CanGoBack)
        {
            _webView.GoBack();
            _messenger.Send(new BrowserBackMessage());
        }
    }
    
    public async void Reload()
    {
        var token = _cancellationTokenSource.Token;
        await Reload(token);
    }

    public void Push(INavigateOptions options)
    {
        var uri = UriConverter.ToUri(options.Address);
        _webView.CoreWebView2.Navigate(uri.ToString());
    }
    
    private Uri CreateUri(string address)
    {
        var uri = new Uri(address);
        return uri;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _webView.SourceChanged -= WebViewOnSourceChanged;
            
            _webView.NavigationStarting -= OnNavigationStarting;
            _webView.NavigationCompleted -= OnNavigationCompleted;
            
            _cancellationTokenSource.Cancel();
            
            _disposables.Dispose();
            
            _logger.LogInformation("Page disposed");
        }
    }
}