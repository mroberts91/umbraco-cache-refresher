namespace Our.Umbraco.CacheRefresher;

public abstract class NotifiableAsyncCacheRefresher<TNotification> : INotifiableAsyncCacheRefresher<TNotification>
    where TNotification : INotification
{
    public async Task HandleAsync(TNotification notification, CancellationToken cancellationToken) => await OnNotificationReceivedAsync(notification, cancellationToken);

    protected abstract Task OnNotificationReceivedAsync(TNotification notification, CancellationToken cancellationToken);
}
