using System.Windows.Input;
using Browser.Core.Commands;
using PresenterBase.ViewModel;

namespace BrowserApp;

internal class MainViewModel : ViewModelBase
{
    public ICommand AddTabCommand { get; }
    public ICommand CloseTabCommand { get; }
    
    public ICommand ReloadTabCommand { get; }

    public MainViewModel(AddBrowserPageCommand addBrowserPageCommand,
        RemoveBrowserPageCommand removeBrowserPageCommand,
        ReloadBrowserPageCommand reloadBrowserPageCommand)
    {
        AddTabCommand = addBrowserPageCommand;
        CloseTabCommand = removeBrowserPageCommand;
        ReloadTabCommand = reloadBrowserPageCommand;
    }
}