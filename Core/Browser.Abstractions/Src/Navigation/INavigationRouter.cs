﻿namespace Browser.Abstractions.Navigation;

public interface INavigationRouter : IPathObservable
{
    void Forward();
    
    bool CanForward { get; }


    void Back();
    
    
    bool CanBack { get; }
    
    
    void Reload();
    
    bool CanReload { get; }
    
    
    void Push(INavigateOptions options);
}

public static class NavigationRouterExtensions
{
    public static void Navigate(this INavigationRouter router, string address)
    {
        router.Push(new NavigateOptions(address));
    }
}