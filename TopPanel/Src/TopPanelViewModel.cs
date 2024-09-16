using System;
using System.Reactive.Disposables;
using System.Windows.Input;
using Browser.Core.Navigation;
using CommunityToolkit.Mvvm.Input;
using PresenterBase.ViewModel;
using TopPanel.TabsPanel;

namespace TopPanel;

internal class TopPanelViewModel : ViewModelBase
{
    public TabsPanelViewModel TabsPanelViewModel { get; }
    
    
    private readonly IBrowserRouter _browserRouter;
    
    private string _searchAddress;
    public string SearchAddress
    {
        get => _searchAddress;
        set => SerProperty(ref _searchAddress, value);
    }
    
    public ICommand SearchCommand { get; }
    
    
    public ICommand BackCommand => _backCommand;
    public ICommand ForwardCommand => _forwardCommand;
    public ICommand RefreshCommand => _refreshCommand;
    
    private readonly RelayCommand _backCommand;
    private readonly RelayCommand _forwardCommand;
    private readonly RelayCommand _refreshCommand;
    
    private readonly CompositeDisposable _disposables = new();

    public TopPanelViewModel(IBrowserRouter browserRouter)
    {
        _searchAddress = string.Empty;
        _browserRouter = browserRouter;
        
        _disposables.Add(browserRouter.Path.Subscribe(OnPathChanged));  
        
        SearchCommand = new RelayCommand(OnSearch);
        
        _backCommand = new RelayCommand(DoBack, ()=> browserRouter.CanBack);
        _forwardCommand = new RelayCommand(DoForward, ()=> browserRouter.CanForward);
        _refreshCommand = new RelayCommand(DoRefresh, ()=>browserRouter.CanRefresh);
        
        TabsPanelViewModel = new TabsPanelViewModel();
    }
    
    private void OnPathChanged(string path)
    {
        SearchAddress = path;
    }

    private void DoBack()
    {
        _browserRouter.Back();
        _backCommand.NotifyCanExecuteChanged();
        _forwardCommand.NotifyCanExecuteChanged();
    }
    
    private void DoForward()
    {
        _browserRouter.Forward();
        _forwardCommand.NotifyCanExecuteChanged();
        _backCommand.NotifyCanExecuteChanged();
    }
    
    private void DoRefresh()
    {
        _browserRouter.Refresh();
        _refreshCommand.NotifyCanExecuteChanged();
    }

    private void OnSearch()
    {
        _browserRouter.Navigate(SearchAddress);
        
        _backCommand.NotifyCanExecuteChanged();
        _forwardCommand.NotifyCanExecuteChanged();
        _refreshCommand.NotifyCanExecuteChanged();
    }
}