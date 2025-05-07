using Microsoft.AspNetCore.Mvc;
using redis_teste.Models;
using redis_teste.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
builder.Services.AddSingleton<IRedisUtil, RedisUtil>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/get/{key}", async (string key, IRedisUtil redisUtil) =>
{
    var resultado = await redisUtil.GetAsync<string>(key);

    if (resultado is null)
        return Results.NotFound("Chave não encontrada.");

    return Results.Ok(resultado);
});

app.MapPost("/set/{key}", async (string key, [FromBody] Request req, IRedisUtil redisUtil) =>
{
    if (string.IsNullOrWhiteSpace(req.Conteudo))
        return Results.BadRequest("O conteúdo não pode ser vazio.");

    await redisUtil.SetAsync(key, req.Conteudo);
    return Results.Ok("Chave salva com sucesso.");
});


app.Run();
