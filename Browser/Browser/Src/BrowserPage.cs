using Browser.Core;
using Browser.Core.Navigation;

namespace Browser;

internal class BrowserPage : IBrowserPage
{
    public string Path { get;  private set; }
    
    public void Forward()
    {
    }

    public void Back()
    {
    }

    public void Refresh()
    {
    }

    public void Push(INavigateOptions options)
    {
        Path = options.Address;
    }

    public void Replace(INavigateOptions options)
    {
        Path = options.Address;
    }
}