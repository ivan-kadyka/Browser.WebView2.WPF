using Browser.Abstractions.Navigation;

namespace Browser.Messenger;

public class BrowserForwardMessage
{
    
}

public class BrowserBackMessage
{
    
}

public class BrowserRefreshMessage
{
    
}

public class NavigationPathChangedMessage
{
    public string Address { get; }

    public NavigationPathChangedMessage(INavigateOptions options)
    {
        Address = options.Address;
    }
}