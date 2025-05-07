using Microsoft.Extensions.Options;
using redis_teste.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace redis_teste.Utils;

public class RedisUtil : IRedisUtil, IAsyncDisposable
{
    private readonly ConnectionMultiplexer? _redis;
    private readonly bool _isEnabled;
    private readonly RedisOptions _redisOptions;
    private readonly string _prefix = "ID:";

    public RedisUtil(IOptions<RedisOptions> options)
    {
        _redisOptions = options.Value;

        _isEnabled = _redisOptions.Enabled;
        if (_isEnabled)
        {
            try
            {
                Console.WriteLine(_redisOptions.ConnectionString);

                _redis = ConnectionMultiplexer.Connect(_redisOptions.ConnectionString);
                Console.WriteLine("Conexão com o Redis estabelecida com sucesso.");
                Console.WriteLine($"Persistência dos dados: {_redisOptions.Persistencia}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível conectar ao Redis. Redis será desativado.\n" + ex.ToString());
                _isEnabled = false;
            }
        }
    }

    public async Task<RedisGetObject<T>?> GetAsync<T>(string key) where T : class
    {
        if (!_isEnabled || _redis is null)
            return null;

        var chave = $"{_prefix}{key}";

        try
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(chave);
            Console.WriteLine($"Valor recuperado com sucesso para a chave {chave}.");

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            var objeto = JsonSerializer.Deserialize<RedisSetObject>(value.ToString());

            if (objeto is null)
                return null;

            var conteudo = JsonSerializer.Deserialize<T>(objeto.Conteudo);

            if (conteudo is null)
                return null;

            var retorno = new RedisGetObject<T>
            {
                Conteudo = conteudo,
                Timestamp = objeto.Timestamp
            };

            return retorno;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tentar recuperar o valor no Redis para a chave {key}.\n" + ex.ToString());
            return null;
        }
    }

    public async Task<RedisGetObject<T>?> GetAsync<T>(ERedis key) where T : class
    {
        if (!_isEnabled || _redis is null)
            return null;

        var chave = $"{_prefix}{key}";

        try
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(chave);
            Console.WriteLine("Valor recuperado com sucesso para a chave {Key}.", chave);

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            var objeto = JsonSerializer.Deserialize<RedisSetObject>(value.ToString());

            if (objeto is null)
                return null;

            var conteudo = JsonSerializer.Deserialize<T>(objeto.Conteudo);

            if (conteudo is null)
                return null;

            var retorno = new RedisGetObject<T>
            {
                Conteudo = conteudo,
                Timestamp = objeto.Timestamp
            };

            return retorno;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tentar recuperar o valor no Redis para a chave {chave}.\n" + ex.ToString());
            return null;
        }
    }

    public async Task SetAsync(ERedis key, object value, TimeSpan? tempoDuracao = null)
    {
        if (!_isEnabled || _redis is null)
            return;

        var chave = $"{_prefix}{key}";
        
        try
        {
            var objeto = new RedisSetObject
            {
                Conteudo = JsonSerializer.Serialize(value)
            };

            var valor = JsonSerializer.Serialize(objeto);
            var db = _redis.GetDatabase();

            await db.StringSetAsync(chave, valor, tempoDuracao is null ? _redisOptions.Persistencia : tempoDuracao);
            Console.WriteLine($"Valor definido com sucesso para a chave {chave}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tentar definir o valor no Redis para a chave {chave}.\n" + ex.ToString());
        }
    }

    public async Task SetAsync(string key, object value, TimeSpan? tempoDuracao = null)
    {
        if (!_isEnabled || _redis is null)
            return;

        var chave = $"{_prefix}{key}";
        
        try
        {
            var objeto = new RedisSetObject
            {
                Conteudo = JsonSerializer.Serialize(value)
            };

            var valor = JsonSerializer.Serialize(objeto);
            var db = _redis.GetDatabase();

            await db.StringSetAsync(chave, valor, tempoDuracao is null ? _redisOptions.Persistencia : tempoDuracao);
            Console.WriteLine($"Valor definido com sucesso para a chave {chave}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tentar definir o valor no Redis para a chave {chave}.\n" + ex.ToString());
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_redis is not null)
        {
            await _redis.CloseAsync();
            _redis.Dispose();
            Console.WriteLine("Conexão com o Redis foi encerrada.");
        }

        GC.SuppressFinalize(this);
    }
}
