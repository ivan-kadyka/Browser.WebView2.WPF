using System.Reactive.Disposables;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Reactive.Extensions.Observable;
using Browser.Abstractions.Settings;
using Browser.Core.UriResolver;
using Browser.Messenger;
using Browser.Messenger.Navigation;
using CommunityToolkit.Mvvm.Messaging;
using Disposable;
using Microsoft.Extensions.Logging;

namespace Browser.App.Tests.Stubs
{
    internal class WebPageStub : DisposableBase, IBrowserPage
    {
        private readonly IMessenger _messenger;
        private readonly IBrowserPageSettings _settings;
        private readonly IUriResolver _uriResolver;
        private readonly ILogger _logger;
        private readonly UndoRedoStack<Uri> _navigationHistory;

        public IObservableValue<Uri> Source => _navigationHistory.Current;
        public PageId Id { get; }
        public string Title => _navigationHistory.Current.Value.Host;
        public object Content { get; private set; }
        
        private readonly CompositeDisposable _disposables = new();
        
        public WebPageStub(
            PageId id,
            IMessenger messenger,
            IBrowserPageSettings settings,
            IUriResolver uriResolver,
            ILogger logger)
        {
            Id = id;
            
            _messenger = messenger;
            _settings = settings;
            _uriResolver = uriResolver;
            _logger = logger;
            _messenger = messenger;
            _settings = settings;
            _uriResolver = uriResolver;
            _logger = logger;

            _navigationHistory = new UndoRedoStack<Uri>(_settings.Source);
            
            _disposables.Add(_navigationHistory.Current.Subscribe(uri =>
            {
                _messenger.Send(new NavigationStartingMessage());
                _messenger.Send(new NavigationCompletedMessage(true));
            }));
        }
        

        public bool CanForward => _navigationHistory.CanRedo;
        public bool CanBack => _navigationHistory.CanUndo;
        public bool CanReload => true;

        public void Forward()
        {
            if (CanForward)
            {
                _navigationHistory.Redo();
                _messenger.Send(new BrowserForwardMessage());
            }
        }

        public void Back()
        {
            if (CanBack)
            {
                _navigationHistory.Undo();
                _messenger.Send(new BrowserBackMessage());
            }
        }

        public void Reload()
        {
            // Reloading can be a no-op in this stub
        }

        public void Push(INavigateOptions options)
        {
            var uri = _uriResolver.GetUri(options.Address);
            _navigationHistory.Do(uri);
        }

        public Task Load(CancellationToken token = default)
        {
            return Task.CompletedTask;
        }

        public Task Reload(CancellationToken token)
        {
            return Task.CompletedTask;
        }



        protected override void Dispose(bool disposing)
        {
          if (disposing)
          {
              _disposables.Dispose();
              _logger.LogInformation("Page disposed");
          }
        }
    }
}