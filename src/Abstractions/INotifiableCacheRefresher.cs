namespace Our.Umbraco.CacheRefresher.Abstractions;
public interface INotifiableCacheRefresher<in TNotification> : INotificationHandler<TNotification> 
    where TNotification : INotification
{
}
