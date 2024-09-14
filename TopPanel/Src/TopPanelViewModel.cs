using System;
using System.Windows.Input;
using PresenterBase.ViewModel;
using TopPanel.Command;

public class TopPanelViewModel : ViewModelBase
{
    public string Title { get; set; } = "TopPanel Title";
    
    private string _searchAddress;
    public string SearchAddress
    {
        get => _searchAddress;
        set => SetField(ref _searchAddress, value);
    }

    // Define the command
    public ICommand SearchCommand { get; }

    public TopPanelViewModel()
    {
        SearchCommand = new RelayCommand(OnSearch);
    }

    private void OnSearch()
    {
        // Implement the action to be taken when Enter is pressed
        // For example, you could search or process the SearchAddress
        System.Diagnostics.Debug.WriteLine($"Searching for: {SearchAddress}");
    }
    
    public void OnEnterKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Console.WriteLine("Enter" + _searchAddress);
            SearchCommand.Execute(null);
        }
    }
}