using System.Windows;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace BrowserApp
{
    public partial class App
    {
        private IPresenter _mainPresenter;

        private readonly ServiceProvider _serviceProvider;
        
        public App()
        {
            _serviceProvider = AppServiceProvider.Create();
        }
        
        protected override async void OnStartup(StartupEventArgs e)
        { 
            base.OnStartup(e);

            _mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();
            await _mainPresenter.Start();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider.Dispose();
            base.OnExit(e);
        }
    }
}