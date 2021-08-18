namespace Our.Umbraco.CacheRefresher.Abstractions;

public interface INotifiableAsyncCacheRefresher<in TNotification> : INotificationAsyncHandler<TNotification>
    where TNotification : INotification
{
}
