using PresenterBase.View;

namespace BrowserApp;

internal interface IMainWindow : IView
{
    void SetTopPanelContent(object content);
    
    void SetPageContent(object content);
    
    void Show();
}