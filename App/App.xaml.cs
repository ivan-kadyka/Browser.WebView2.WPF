using System.Windows;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace BrowserApp
{
    public partial class App
    {
        private readonly IPresenter _mainPresenter;

        private readonly ServiceProvider _serviceProvider;
        
        public App()
        {
            _serviceProvider = AppServiceProvider.Create();
            _mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();
        }
        
        protected override async void OnStartup(StartupEventArgs e)
        { 
            base.OnStartup(e);

            await _mainPresenter.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider.Dispose();
            base.OnExit(e);
        }
    }
}