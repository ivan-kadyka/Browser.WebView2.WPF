using System;
using BrowserApp.Module;
using Microsoft.Extensions.DependencyInjection;

namespace BrowserApp;

internal class AppServiceProvider : IServiceProvider, IDisposable
{
    private static AppServiceProvider? _instance;

    private readonly ServiceProvider _serviceProvider;
    private static readonly object _locker = new();

    private AppServiceProvider(Action<IServiceCollection>? registerCallback = null)
    {
        var services = new ServiceCollection();
        services.AddBrowserAppModule();
        
        if (registerCallback != null)
        {
            registerCallback(services);
        }
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    public object? GetService(Type serviceType)
    {
        return _serviceProvider.GetService(serviceType);
    }

    public static AppServiceProvider Create()
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = new AppServiceProvider();
                }
            }
        }

        return _instance;
    }
    
    internal static AppServiceProvider CreateTestInstance(Action<IServiceCollection>? registerCallback = null)
    {
        if (_instance == null)
        {
            _instance = new AppServiceProvider(registerCallback);
        }
        
        return _instance;
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
        _instance = default;
    }
}