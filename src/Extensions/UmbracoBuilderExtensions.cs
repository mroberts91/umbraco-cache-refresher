namespace Umbraco.Extensions;
public static class UmbracoBuilderExtensions
{
    public static IUmbracoBuilder AddCacheRefresher<TNotification, TNotificationHandler>(this IUmbracoBuilder builder)
        where TNotification: INotification
        where TNotificationHandler : INotifiableCacheRefresher<TNotification>
    {
        builder.AddNotificationHandler<TNotification, TNotificationHandler>();
        return builder;
    }

    public static IUmbracoBuilder AddAsyncCacheRefresher<TNotification, TNotificationAsyncHandler>(this IUmbracoBuilder builder)
        where TNotification : INotification
        where TNotificationAsyncHandler : INotifiableAsyncCacheRefresher<TNotification>
    {
        builder.AddNotificationAsyncHandler<TNotification, TNotificationAsyncHandler>();
        return builder;
    }
}
