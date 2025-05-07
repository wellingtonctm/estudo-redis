namespace redis_teste.Models;

public class RedisSetObject
{
    public required string Conteudo { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

public class RedisGetObject <T> where T : class
{
    public required T Conteudo { get; set; }
    public DateTime Timestamp { get; set; }
}