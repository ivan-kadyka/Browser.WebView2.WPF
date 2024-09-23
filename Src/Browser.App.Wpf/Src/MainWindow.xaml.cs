namespace BrowserApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetTopPanelContent(object content)
        {
            TopPanel.Content = content;
        }

        public void SetPageContent(object content)
        {
            Page.Content = content;
        }
    }
}