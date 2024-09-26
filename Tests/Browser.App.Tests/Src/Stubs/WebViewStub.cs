using System.Drawing;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;


namespace Browser.App.Tests.Stubs
{
    public class WebViewStub : IWebView2
    {
        public CoreWebView2CreationProperties CreationProperties { get; set; }
        public CoreWebView2 CoreWebView2 { get; }

        public Uri Source
        {
            get => _navigationHistory.Current.Value;
            set => _navigationHistory.Do(value);
        }

        public bool CanGoBack => _navigationHistory.CanUndo;
        public bool CanGoForward => _navigationHistory.CanRedo;
        public double ZoomFactor { get; set; }
        public Color DefaultBackgroundColor { get; set; }
        public Color DesignModeForegroundColor { get; set; }
        public bool AllowExternalDrop { get; set; }
        public event EventHandler<CoreWebView2SourceChangedEventArgs> SourceChanged;
        public event EventHandler<CoreWebView2NavigationStartingEventArgs> NavigationStarting;
        public event EventHandler<CoreWebView2NavigationCompletedEventArgs> NavigationCompleted;
        public event EventHandler<EventArgs>? ZoomFactorChanged;
        public event EventHandler<CoreWebView2ContentLoadingEventArgs> ContentLoading;
        public event EventHandler<CoreWebView2WebMessageReceivedEventArgs> WebMessageReceived;
        public event EventHandler<CoreWebView2InitializationCompletedEventArgs> CoreWebView2InitializationCompleted;

        private UndoRedoStack<Uri> _navigationHistory;

        public WebViewStub()
        {
            CreationProperties = new CoreWebView2CreationProperties();
            _navigationHistory    = new(new Uri("about:blank"));
            ZoomFactor = 1.0;
            DefaultBackgroundColor = Color.White;
            DesignModeForegroundColor = Color.Gray;
            AllowExternalDrop = true;
        }

        public Task EnsureCoreWebView2Async(CoreWebView2Environment environment = null,
            CoreWebView2ControllerOptions controllerOptions = null)
        {
            // Simulate async WebView2 initialization
            return Task.CompletedTask;
        }

        public Task EnsureCoreWebView2Async(CoreWebView2Environment environment)
        {
            // Simulate async WebView2 initialization
            return Task.CompletedTask;
        }

        public void BeginInit()
        {
            // No-op for initialization stub
        }

        public void EndInit()
        {
            // No-op for initialization stub
        }

        public void GoBack()
        {
            if (CanGoBack)
            {
                _navigationHistory.Undo();

                // Raise events for the navigation change
                OnSourceChanged();
                OnNavigationCompleted(true);
            }
        }

        public void GoForward()
        {
            if (CanGoForward)
            {
                _navigationHistory.Redo();

                // Raise events for the navigation change
                OnSourceChanged();
                OnNavigationCompleted(true);
            }
        }

        public void Reload()
        {
            // Simulate reloading the current page
            if (Source != null)
            {
                // Raise the NavigationStarting event
                OnNavigationStarting();

                // Raise the NavigationCompleted event (simulating success)
                OnNavigationCompleted(true);
            }
        }

        public void Stop()
        {
            // Simulate stopping the current navigation
        }

        public void NavigateToString(string htmlContent)
        {
            // Simulate navigating to a string containing HTML content
            var newUri = new Uri("about:blank");
            _navigationHistory.Do(newUri);

            // Raise events for navigation change
            OnSourceChanged();
            OnNavigationCompleted(true);
        }

        public Task<string> ExecuteScriptAsync(string javaScript)
        {
            // Simulate execution of a JavaScript script
            return Task.FromResult(string.Empty);
        }

        public bool Focus()
        {
            // Simulate focusing the WebView control
            return false;
        }

        public void Dispose()
        {
            // Clean up resources
        }

        // Helper methods to raise events

        private void OnSourceChanged()
        {
         //   SourceChanged?.Invoke(this, new CoreWebView2SourceChangedEventArgs());
        }

        private void OnNavigationStarting()
        {
        //    NavigationStarting?.Invoke(this, new CoreWebView2NavigationStartingEventArgs());
        }

        private void OnNavigationCompleted(bool success)
        {
         //   NavigationCompleted?.Invoke(this, new CoreWebView2NavigationCompletedEventArgs(success));
        }
    }
}
