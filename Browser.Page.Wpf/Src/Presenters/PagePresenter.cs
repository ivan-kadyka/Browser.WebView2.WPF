using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Browser.Page.Wpf.Page;
using Disposable;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Presenters;

internal class PagePresenter : DisposableBase, IPresenter
{
    private readonly IBrowserPage _page;
    public object Content { get; }
    
    private readonly IBrowserPathRouter _browserPathRouter;
    private readonly PageView _view;
    private readonly PageViewModel _viewModel;
    
    private readonly CompositeDisposable _disposables = new();
    
    public PagePresenter(IBrowserPage page)
    {
        _page = page;
        
        _view = new PageView(page);
        _viewModel = new PageViewModel(page, page.Path.Value.Address);
        _view.DataContext = _viewModel;
        _view.WebView.Content = _viewModel.Content;
        
        Content = _view;
    }
    

/*
    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
        AddDisposable(_browserPathRouter.Path.Subscribe(options =>
        {
            _view.Navigate(options.Address);
        }));
    }
    
    */

    public async Task Start(CancellationToken token = default)
    {
        await  _view.Show();
     //  await _page.Load(token);
    }

    public async Task Stop(CancellationToken token = default)
    {
        await  _view.Hide();
     //   return Task.CompletedTask;
    }
    
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _disposables.Dispose();
        }
    }
}