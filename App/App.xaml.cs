using System.Windows;
using PresenterBase.Presenter;
using WpfApp1.Main;
using WpfApp1.Presenters;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private readonly IPresenter _mainPresenter;
        
        public App()
        {
            var mainViewModel = new MainViewModel();
            _mainPresenter = new MainPresenter(mainViewModel);
        }
        
        protected override async void OnStartup(StartupEventArgs e)
        { 
            base.OnStartup(e);

            await _mainPresenter.Start();
        }
    }
}