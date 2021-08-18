namespace Umbraco.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCacheRefresher<TNotification, TCahceRefresher>(this IServiceCollection services)
        where TNotification: INotification
        where TCahceRefresher: INotifiableCacheRefresher<TNotification>
    {
        return services;
    }
}
