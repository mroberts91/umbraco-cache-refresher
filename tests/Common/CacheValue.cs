namespace Our.Umbraco.CacheRefresher.Tests.Common;

public class CacheValue
{
    public int? Id { get; set; }
    public string? Name { get; set; }


    public override bool Equals(object? obj)
    {
        return obj switch
        {
            CacheValue cv when cv.Name == Name && cv.Id == Id => true,
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
