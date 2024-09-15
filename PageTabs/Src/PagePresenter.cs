using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using Browser.Core.Navigation;
using PresenterBase.Presenter;

internal class PagePresenter : PresenterBase<PageView>
{
    private readonly IBrowserPathRouter _browserPathRouter;
    private readonly CompositeDisposable _disposables = new();
    
    public PagePresenter(PageViewModel viewModel, IBrowserPathRouter browserPathRouter) : base(new PageView(), viewModel)
    {
        _browserPathRouter = browserPathRouter;
    }

    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
         await ProtectedView.Start();
        
        _disposables.Add(_browserPathRouter.Path.Subscribe(path =>
        {
            ProtectedView.Navigate(path);
        }));
    }
}