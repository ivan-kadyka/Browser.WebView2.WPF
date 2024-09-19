using System.Windows.Input;
using Browser.Abstractions.Navigation;
using Browser.Core.Commands;
using Browser.Messenger;
using Browser.Messenger.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PresenterBase.ViewModel;

namespace Browser.TopPanel.Wpf.NavigationPanel;

internal class NavigationPanelViewModel : ObservableRecipient, IViewModel,
    IRecipient<BrowserForwardMessage>,
    IRecipient<BrowserBackMessage>,
    IRecipient<NavigationStartingMessage>,
    IRecipient<NavigationCompletedMessage>,
    IRecipient<BrowserActivePageChangedMessage>
{
    public ICommand BackCommand => _backCommand;
    public ICommand ForwardCommand => _forwardCommand;
    
    public ICommand ReloadCommand  {get;}
    
    public string ReloadIconSource => _isNavigation  ? CrossIcon  : RotateIcon;
    
    private readonly RelayCommand _backCommand;
    private readonly RelayCommand _forwardCommand;

    private bool _isNavigation;

    private bool IsNavigation
    {
        set
        {
            _isNavigation = value;
           OnPropertyChanged(nameof(ReloadIconSource));
        }
    }
    
    private const string ImagePath = "pack://application:,,,/Browser.TopPanel.Wpf;component/Images/";
    private const string RotateIcon = ImagePath + "rotate-cw.svg";
    private const string CrossIcon = ImagePath + "cross-icon.svg";
    
    public NavigationPanelViewModel(IBrowserRouter browserRouter,
        IMessenger messenger,
        ReloadBrowserPageCommand reloadCommand) 
        : base(messenger)
    {
        ReloadCommand = reloadCommand;
        _backCommand = new RelayCommand(browserRouter.Back, ()=> browserRouter.CanBack);
        _forwardCommand = new RelayCommand(browserRouter.Forward, ()=> browserRouter.CanForward);
    }

    private void NavigationControlsNotify()
    {
        _forwardCommand.NotifyCanExecuteChanged();
        _backCommand.NotifyCanExecuteChanged();
    }
    
    public void Receive(NavigationStartingMessage message)
    {
        IsNavigation = true;
        NavigationControlsNotify();
    }

    public void Receive(NavigationCompletedMessage message)
    {
        IsNavigation = false;
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