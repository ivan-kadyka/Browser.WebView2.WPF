using Browser.Core.Navigation;

namespace Browser.Messages;

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