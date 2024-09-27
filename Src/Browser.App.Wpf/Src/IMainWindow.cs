using PresenterBase.View;

namespace BrowserApp;

public interface IMainWindow : IView
{
    void SetTopPanelContent(object content);
    
    void SetPageContent(object content);
    
    void Show();
    
    void Close();
}