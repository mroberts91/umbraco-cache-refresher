namespace Our.Umbraco.CacheRefresher;

public abstract class ContentTreeChangedCacheRefresher : NotifiableCacheRefresher<ContentTreeChangeNotification>
{
    /// <inheritdoc />
    protected override void OnNotificationReceived(ContentTreeChangeNotification notification)
    {
        if (notification?.Changes is not IEnumerable<TreeChange<IContent>> treeChanges) return;

        foreach (TreeChange<IContent> treeChange in treeChanges)
        {
            if (treeChange?.Item is not IContent content) continue;

            ContentChanged(content, treeChange.ChangeTypes);
        }
    }

    protected abstract void ContentChanged(IContent content, TreeChangeTypes treeChangeType);
    
}
