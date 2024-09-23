using System;
using Microsoft.Extensions.DependencyInjection;

namespace BrowserApp.Module;

internal class AppServiceProvider
{
    private static ServiceProvider? _instance;
    
    private AppServiceProvider(){}

    public static ServiceProvider Create(Action<IServiceCollection>? registerCallback = null)
    {
        if (_instance == null)
        {
            _instance = CreateInternalServiceProvider(registerCallback);
        }

        return _instance;
    }

    private  static ServiceProvider CreateInternalServiceProvider(Action<IServiceCollection>? registerCallback = null)
    {
        var services = new ServiceCollection();
        services.AddAppServices();
        
        if (registerCallback != null)
        {
            registerCallback(services);
        }
        
        return services.BuildServiceProvider();
    }
}