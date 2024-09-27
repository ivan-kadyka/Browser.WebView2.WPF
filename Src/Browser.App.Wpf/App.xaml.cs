using System.Windows;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace BrowserApp
{
    public partial class App
    {
        private IPresenter? _mainPresenter;

        private readonly AppServiceProvider _serviceProvider;
        
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


        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            
            if (_mainPresenter != null)
                await _mainPresenter.Stop();
         
            _serviceProvider.Dispose();
        }
    }
}