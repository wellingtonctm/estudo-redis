namespace redis_teste.Models;

public class RedisOptions
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public TimeSpan Persistencia { get; set; } = TimeSpan.FromDays(7);
}