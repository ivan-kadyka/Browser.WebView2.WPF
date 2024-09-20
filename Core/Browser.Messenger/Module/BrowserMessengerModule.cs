using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Messenger.Module;

public static class BrowserMessengerModule
{
    public static IServiceCollection AddBrowserMessengerModule(this IServiceCollection services)
    {
        services.AddSingleton<IMessenger, StrongReferenceMessenger>();
        
        return services;
    }
}