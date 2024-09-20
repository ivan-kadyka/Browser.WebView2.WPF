using System.Windows.Input;
using Browser.Core.Commands;
using PresenterBase.ViewModel;

namespace BrowserApp;

internal class MainViewModel : ViewModelBase
{
    public ICommand CreateTabCommand { get; }
    public ICommand CloseTabCommand { get; }
    
    public ICommand ReloadTabCommand { get; }

    public MainViewModel(CreateBrowserPageCommand createBrowserPageCommand,
        RemoveBrowserPageCommand removeBrowserPageCommand,
        ReloadBrowserPageCommand reloadBrowserPageCommand)
    {
        CreateTabCommand = createBrowserPageCommand;
        CloseTabCommand = removeBrowserPageCommand;
        ReloadTabCommand = reloadBrowserPageCommand;
    }
}