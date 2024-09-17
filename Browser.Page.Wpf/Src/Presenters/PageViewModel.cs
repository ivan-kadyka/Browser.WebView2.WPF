using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.Page.Wpf.Page;

internal class PageViewModel : ViewModelBase
{
    public object Content { get; }
    
    public PageViewModel(IBrowserPage page)
    {
        Content = page.Content;
    }
}