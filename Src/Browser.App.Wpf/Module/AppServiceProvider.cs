using System;
using Microsoft.Extensions.DependencyInjection;

namespace BrowserApp.Module;

internal class AppServiceProvider : IServiceProvider
{
    private static AppServiceProvider? _instance;

    private readonly IServiceProvider _serviceProvider;
    private static readonly object _locker = new();

    private AppServiceProvider(Action<IServiceCollection>? registerCallback = null)
    {
        var services = new ServiceCollection();
        services.AddAppServices();
        
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

    public static IServiceProvider Create(Action<IServiceCollection>? registerCallback = null)
    {
        if (_instance == null)
        {
            lock (_locker)
            {
                if (_instance == null)
                {
                    _instance = new AppServiceProvider(registerCallback);
                }
            }
        }

        return _instance;
    }
}