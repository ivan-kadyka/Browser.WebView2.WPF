using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Browser.Messenger.Module;

public static class MessagesModule
{
    public static IServiceCollection AddMessagesServices(this IServiceCollection services)
    {
        services.AddSingleton<IMessenger, StrongReferenceMessenger>();
        
        return services;
    }
}