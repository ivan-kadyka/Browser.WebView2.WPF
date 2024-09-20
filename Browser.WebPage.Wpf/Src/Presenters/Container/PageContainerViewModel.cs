using Browser.Abstractions.Page;
using PresenterBase.ViewModel;

namespace Browser.WebPage.Wpf.Presenters.Container;

public class PageContainerViewModel : ViewModelBase
{
    public object WebContent
    {
        get => _content;
        set
        {
            if (_content == value)
                return;
            
            _content = value;
            OnPropertyChanged();
        }
    }
    
    
    private object _content;
    
    public PageContainerViewModel(IPage page)
    {
        _content =  page.Content;
    }
}