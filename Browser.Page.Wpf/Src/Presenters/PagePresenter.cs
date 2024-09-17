using System;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Navigation;
using Browser.Page.Wpf.Page;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Presenters;

internal class PagePresenter : Presenter
{
    private readonly IBrowserPathRouter _browserPathRouter;
    private readonly PageView _view;
    
    public PagePresenter(IBrowserPathRouter browserPathRouter, PageViewModel viewModel)
        : this(browserPathRouter, viewModel, new PageView())
    {
    }
    
    public PagePresenter(IBrowserPathRouter browserPathRouter,  PageViewModel viewModel,
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