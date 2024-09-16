using Browser.Core;
using Browser.Core.Navigation;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Browser.Commands;

public class AddBrowserPageCommand : IRelayCommand<INavigateOptions>
{
    private readonly IBrowser _browser;
    private readonly IMessenger _messenger;
    private readonly RelayCommand<INavigateOptions> _command;

    public AddBrowserPageCommand(IBrowser browser, IMessenger messenger)
    {
        _browser = browser;
        _messenger = messenger;
        _command = new RelayCommand<INavigateOptions>(Execute);
    }
    
    public void Execute(INavigateOptions? parameter)
    {
        if (parameter == null)
        {
            parameter = new UrlNavigateOptions("");
        }
        
        _browser.AddPage(new BrowserPage(_messenger, parameter));
    }
    
    public bool CanExecute(object? parameter)
    {
        return _command.CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
       _command.Execute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => _command.CanExecuteChanged += value;
        remove => _command.CanExecuteChanged -= value;
    }
    
    public void NotifyCanExecuteChanged()
    {
        _command.NotifyCanExecuteChanged();
    }

    public bool CanExecute(INavigateOptions? parameter)
    {
        return _command.CanExecute(parameter);
    }
}


    