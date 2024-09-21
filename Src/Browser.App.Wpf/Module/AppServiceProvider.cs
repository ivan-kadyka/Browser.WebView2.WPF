using Microsoft.Extensions.DependencyInjection;

namespace BrowserApp.Module;

internal class AppServiceProvider
{
    private static ServiceProvider? _instance;
    
    private AppServiceProvider(){}

    public static ServiceProvider Create()
    {
        if (_instance == null)
        {
            _instance = CreateInternalServiceProvider();
        }

        return _instance;
    }

    private  static ServiceProvider CreateInternalServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddAppServices();
        
        return services.BuildServiceProvider();
    }
}