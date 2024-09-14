using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PresenterBase.Presenter;

namespace BrowserApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IPresenter _mainPresenter;

        private readonly ServiceProvider _serviceProvider;
        
        public App()
        {
            _serviceProvider = ConfigureServices();

            _mainPresenter = _serviceProvider.GetRequiredService<MainPresenter>();
        }
        
        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();
            
            services.AddSingleton<MainPresenter>();

            var provider = services.BuildServiceProvider();

            return provider;
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