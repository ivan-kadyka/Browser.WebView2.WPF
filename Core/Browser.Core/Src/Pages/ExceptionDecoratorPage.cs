using Browser.Abstractions.Exceptions;
using Browser.Abstractions.Navigation;
using Browser.Abstractions.Page;
using Disposable;
using Microsoft.Extensions.Logging;
using Reactive.Extensions.Observable;

namespace Browser.Core.Pages;

public class ExceptionDecoratorPage : DisposableBase, IBrowserPage
{
    public IObservableValue<Uri> Source => _page.Source;
    public PageId Id => _page.Id;
    public string Title => _page.Title;
    public object Content => _page.Content;
    
    public bool CanForward => _page.CanForward;
    
    public bool CanBack => _page.CanBack;
    
    public bool CanReload => _page.CanReload;
    
    
    private readonly IBrowserPage _page;
    private readonly ILogger _logger;

    public ExceptionDecoratorPage(IBrowserPage page, ILogger logger)
    {
        _page = page;
        _logger = logger;
    }
    public void Forward()
    {
        WrapException(_page.Forward, ()=> (PageError.Forward, "Page forward error"));
    }
    
    public void Back()
    {
        WrapException(_page.Back, ()=> (PageError.Forward, "Page back error"));
    }

   
    public void Reload()
    {
        WrapException(_page.Reload, ()=> (PageError.Reload, "Page reload error"));
    }


    public void Push(INavigateOptions options)
    {
        WrapException(()=> _page.Push(options), ()=>
        {
            var message = "Page push options: " + options.Address;
            return (PageError.Reload, message);
        });
    }

    public async Task Load(CancellationToken token = default)
    {
        await WrapExceptionAsync(()=>_page.Load(token), ()=> (PageError.Reload, "Page load error"));
    }

    public async Task Reload(CancellationToken token)
    {
        await WrapExceptionAsync(()=>_page.Reload(token), ()=> (PageError.Reload, "Page reload error"));
    }

    private void WrapException(Action action, Func<(PageError, string)> onErrorInfo)
    {
        try
        {
            action();
        }
        catch (BrowserPageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            ThrowBrowserPageException(ex, onErrorInfo);
        }
    }

    private async Task WrapExceptionAsync(Func<Task> action, Func<(PageError, string)> onErrorInfo)
    {
        try
        {
            await action();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (BrowserPageException)
        {
            throw;
        }
        catch (Exception ex)
        {
            ThrowBrowserPageException(ex, onErrorInfo);
        }
    }
    
    private void ThrowBrowserPageException(Exception ex, Func<(PageError, string)> onErrorInfo)
    {
        var (errorType, message) = onErrorInfo();
        _logger.LogError(ex, message);
        throw new BrowserPageException(errorType, message, ex);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _page.Dispose();
        }
    }
}