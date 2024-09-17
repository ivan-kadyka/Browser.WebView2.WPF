using System.Windows.Input;
using Browser.Core.Commands;
using PresenterBase.ViewModel;

internal class MainViewModel : ViewModelBase
{
    public ICommand AddTabCommand { get; }
    public ICommand CloseTabCommand { get; }

    public MainViewModel(AddBrowserPageCommand addBrowserPageCommand, RemoveBrowserPageCommand removeBrowserPageCommand)
    {
        AddTabCommand = addBrowserPageCommand;
        CloseTabCommand = removeBrowserPageCommand;
    }
}