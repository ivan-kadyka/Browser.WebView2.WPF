namespace Browser.Messenger.Navigation;

public class NavigationCompletedMessage
{
    public bool IsSuccess { get; }

    public NavigationCompletedMessage(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}

public class NavigationStartingMessage
{
}