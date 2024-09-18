namespace Browser.Messenger;

public class BrowserForwardMessage
{
}

public class BrowserBackMessage
{
}

public class BrowserReloadPageMessage
{
}

public class BrowserActivePageChangedMessage
{
    public string PageId { get; }
    
    public BrowserActivePageChangedMessage(string pageId)
    {
        PageId = pageId;
    }
}



public class BrowserSearchAddressChangedMessage
{
    public string Address { get; }

    public BrowserSearchAddressChangedMessage(string address)
    {
        Address = address;
    }
}