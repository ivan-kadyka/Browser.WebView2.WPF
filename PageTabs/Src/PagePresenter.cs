using System;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Navigation;
using PresenterBase.Presenter;

internal class PagePresenter : Presenter
{
    private readonly IBrowserPathRouter _browserPathRouter;
    private readonly PageView _view;
    
    public PagePresenter(PageViewModel viewModel, IBrowserPathRouter browserPathRouter)
        : this(viewModel, browserPathRouter, new PageView())
    {
    }
    

    public PagePresenter(PageViewModel viewModel, 
        IBrowserPathRouter browserPathRouter,
        PageView view) : base(view, viewModel)
    {
        _view = view;
        _browserPathRouter = browserPathRouter;
    }


    protected override async Task OnStarted(CancellationToken token = default)
    {
        await base.OnStarted(token);
        
        AddDisposable(_browserPathRouter.Path.Subscribe(options =>
        {
            _view.Navigate(options.Address);
        }));
    }
}