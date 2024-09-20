using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Abstractions.Settings;
using Browser.Messenger;
using Browser.Messenger.Navigation;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Reactive.Extensions.Observable;

namespace Browser.Page.Wpf.Page;

internal class BrowserPage : DisposableBase, IBrowserPage
{
    public PageId Id { get; }

    public string Title => _webView.Source.Host;

    public object Content => _webView;

    public IObservableValue<Uri> Source  => _uriSource;
    
    private readonly IWebView2 _webView;
    private readonly IMessenger _messenger;
    private readonly IBrowserPageSettings _settings;
    private readonly ILogger _logger;
    
    private readonly CompositeDisposable _disposables = new();
    private readonly ObservableValue<Uri> _uriSource;
    
    public BrowserPage(
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
        _messenger.Send(new NavigationCompletedMessage());
    }

    private void WebViewOnSourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        var uri = _webView.Source;
        _uriSource.OnNext(uri);
    }

    public async Task Load(CancellationToken token = default)
    {
        try
        {
            await _webView.EnsureCoreWebView2Async();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Load page failed, pageId: {Id}, pageTitle: {_settings.Source}");
            throw;
        }
    }

    public Task Reload(CancellationToken token = default)
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

    public bool CanForward => _webView.CanGoForward;

    public void Back()
    {
        if (_webView.CanGoBack)
        {
            _webView.GoBack();
            _messenger.Send(new BrowserBackMessage());
        }
    }

    public bool CanBack => _webView.CanGoBack;

    public void Refresh()
    {
    }

    public bool CanRefresh => _webView.Source != null && !string.IsNullOrWhiteSpace(_webView.Source.Host);

    public void Push(INavigateOptions options)
    {
        _webView.CoreWebView2.Navigate(options.Address);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _webView.SourceChanged -= WebViewOnSourceChanged;
            
            _webView.NavigationStarting -= OnNavigationStarting;
            _webView.NavigationCompleted -= OnNavigationCompleted;
            
            _disposables.Dispose();
            
            _logger.LogInformation("Page disposed");
        }
    }
}