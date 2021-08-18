namespace Our.Umbraco.CacheRefresher;
public abstract class NotifiableCacheRefresher<TNotification> : INotifiableCacheRefresher<TNotification>
    where TNotification : INotification
{
    public void Handle(TNotification notification) => OnNotificationReceived(notification);

    /// <summary>
    /// Forwarded notification from the underlying <see cref="INotificationHandler{TNotification}"/>
    /// </summary>
    /// <param name="notification"></param>
    protected abstract void OnNotificationReceived(TNotification notification);
}
