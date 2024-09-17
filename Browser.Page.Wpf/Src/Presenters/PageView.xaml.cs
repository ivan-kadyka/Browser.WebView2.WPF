using System;
using System.Threading;
using System.Threading.Tasks;
using Browser.Abstractions.Page;
using PresenterBase.View;

namespace Browser.Page.Wpf.Presenters;

internal partial class PageView : UserControlView
{
    private readonly IBrowserPage _page;
    private TaskCompletionSource<bool> _tcs = new();
    public PageView(IBrowserPage page)
    {
        _page = page;
        InitializeComponent();
        
        WebView.Content = page.Content;
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
    
        _tcs.TrySetResult(true);
    }

    protected override async Task OnShow(CancellationToken token = default)
    {
        await _tcs.Task;
        await _page.Load(token);
    }
}