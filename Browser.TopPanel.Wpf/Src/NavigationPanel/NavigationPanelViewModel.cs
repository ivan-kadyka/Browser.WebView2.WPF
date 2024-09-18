using System.Windows.Input;
using Browser.Abstractions.Navigation;
using Browser.Core.Commands;
using Browser.Messenger;
using Browser.Messenger.Navigation;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.ViewModel;

namespace Browser.TopPanel.Wpf.NavigationPanel;

internal class NavigationPanelViewModel : ViewModelBase,   
    IRecipient<BrowserForwardMessage>,
    IRecipient<BrowserBackMessage>,
    IRecipient<BrowserReloadPageMessage>,
    IRecipient<NavigationStartingMessage>,
    IRecipient<NavigationCompletedMessage>,
    IRecipient<BrowserActivePageChangedMessage>
{
    public ICommand BackCommand => _backCommand;
    public ICommand ForwardCommand => _forwardCommand;
    public ICommand ReloadCommand  {get;}
    
    private readonly RelayCommand _backCommand;
    private readonly RelayCommand _forwardCommand;
    
    public NavigationPanelViewModel(IBrowserRouter browserRouter,
        ReloadBrowserPageCommand reloadCommand)
    {
        ReloadCommand = reloadCommand;
        _backCommand = new RelayCommand(browserRouter.Back, ()=> browserRouter.CanBack);
        _forwardCommand = new RelayCommand(browserRouter.Forward, ()=> browserRouter.CanForward);
    }
    
    public void Receive(BrowserReloadPageMessage message)
    {
        // _reloadCommand.NotifyCanExecuteChanged();
    }

    private void NavigationControlsNotify()
    {
        _forwardCommand.NotifyCanExecuteChanged();
        _backCommand.NotifyCanExecuteChanged();
    }
    
    public void Receive(NavigationStartingMessage message)
    {
        NavigationControlsNotify();
    }

    public void Receive(NavigationCompletedMessage message)
    {
        NavigationControlsNotify();
    }

    public void Receive(BrowserActivePageChangedMessage message)
    {
        NavigationControlsNotify();
    }
    
    public void Receive(BrowserForwardMessage message)
    {
        NavigationControlsNotify();
    }

    public void Receive(BrowserBackMessage message)
    {
        NavigationControlsNotify();
    }
}