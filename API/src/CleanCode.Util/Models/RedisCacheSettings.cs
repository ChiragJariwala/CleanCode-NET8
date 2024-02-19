namespace CleanCode.Util.Models
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public int DefaultCacheTimeInSeconds { get; set; }
    }
}
