namespace Our.Umbraco.CacheRefresher;

public abstract class ContentTreeChangedAsyncCacheRefresher : NotifiableAsyncCacheRefresher<ContentTreeChangeNotification>
{
    /// <inheritdoc />
    protected override async Task OnNotificationReceivedAsync(ContentTreeChangeNotification notification, CancellationToken cancellationToken)
    {
        if (notification?.Changes is not IEnumerable<TreeChange<IContent>> treeChanges) return;

        var changedTasks = treeChanges.Where(tc => tc.Item is { }).Select(tc => ContentChangedAsync(tc.Item, tc.ChangeTypes, cancellationToken));

        await Task.WhenAll(changedTasks).ConfigureAwait(false);
    }

    protected abstract Task ContentChangedAsync(IContent content, TreeChangeTypes treeChangeType, CancellationToken cancellationToken);

}