using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions;
using Browser.Abstractions.Page;
using Browser.Page.Wpf.Page;
using Disposable;
using PresenterBase.Presenter;

namespace Browser.Page.Wpf.Presenters.Container;

public class PageContainerPresenter : DisposableBase, IPresenter
{
    public object Content
    {
        get
        {
            return _currentPagePresenter.Content;
        }
    }

    private readonly IBrowser _browser;
    private IPresenter _currentPagePresenter;
    
    public PageContainerPresenter(IBrowser browser)
    {
        _browser = browser;

        var currentPage = browser.CurrentPage.Value;
        
        _currentPagePresenter = CreatePagePresenter(currentPage);
    }
   
    public async Task Start(CancellationToken token = default)
    {
        await _currentPagePresenter.Start(token);
    }

    public async Task Stop(CancellationToken token = default)
    {
        await _currentPagePresenter.Stop(token);
    }

    private IPresenter CreatePagePresenter(IBrowserPage page)
    {
        var pageViewModel = new PageViewModel(page);
        return new PagePresenter(_browser, pageViewModel);
    }
    
    protected override void Dispose(bool disposing)
    {
       if (disposing)
       {
           _currentPagePresenter.Dispose();
       }
    }
}

