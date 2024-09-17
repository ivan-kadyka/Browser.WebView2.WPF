using CommunityToolkit.Mvvm.Input;

namespace Browser.Core.Commands;

public abstract class CommandBase<T> : IRelayCommand<T>
{
    public event EventHandler? CanExecuteChanged
    {
        add => _command.CanExecuteChanged += value;
        remove => _command.CanExecuteChanged -= value;
    }
    
    private readonly IRelayCommand<T> _command;
    
    protected CommandBase()
    {
        _command = new RelayCommand<T>(OnExecute, OnCanExecute);
    }
    
    public bool CanExecute(object? parameter)
    {
        return _command.CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _command.Execute(parameter);
    }
    
    public void NotifyCanExecuteChanged()
    {
        _command.NotifyCanExecuteChanged();
    }

    public bool CanExecute(T? parameter)
    {
        return _command.CanExecute(parameter);
    }

    public void Execute(T? parameter)
    {
        _command.Execute(parameter);
    }
    
    protected abstract void OnExecute(T? parameters);

    protected virtual bool OnCanExecute(T? parameters)
    {
        return true;
    }
}