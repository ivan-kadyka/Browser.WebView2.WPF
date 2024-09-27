using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BrowserApp.Module;

internal class AppServiceProvider : IServiceProvider, IAsyncDisposable, IDisposable
{
    private static AppServiceProvider? _instance;

    private readonly ServiceProvider _serviceProvider;
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

    public async ValueTask DisposeAsync()
    {
        await _serviceProvider.DisposeAsync();
        _instance = null;
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
        _instance = null;
    }
}