using PresenterBase.View;

namespace BrowserApp.Main;

public interface IMainWindow : IView
{
    void SetTopPanelContent(object content);
    
    void SetPageContent(object content);
    
    void Show();
    
    void Close();
}