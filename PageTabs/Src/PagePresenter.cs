using System;
using System.Threading;
using System.Threading.Tasks;
using Browser.Core.Navigation;
using PresenterBase.Presenter;

internal class PagePresenter : PresenterBase<PageView>
{
    private readonly IBrowserPathRouter _browserPathRouter;
    
    public PagePresenter(PageViewModel viewModel, IBrowserPathRouter browserPathRouter) : base(new PageView(), viewModel)
    {
        _browserPathRouter = browserPathRouter;
    }

    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
         await _view.Start();
        
        AddDisposable(_browserPathRouter.Path.Subscribe(path =>
        {
            _view.Navigate(path);
        }));
    }
}