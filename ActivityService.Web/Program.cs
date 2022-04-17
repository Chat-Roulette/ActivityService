using ActivityService.Services.Abstractions;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Newtonsoft;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration as IConfiguration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton(x => new RedisConfiguration()
{
    ConnectionString = configuration.GetSection("Redis")["ConnectionString"]
});
builder.Services.AddSingleton<ISerializer, NewtonsoftSerializer>();
builder.Services.AddSingleton<IRedisConnectionPoolManager, RedisConnectionPoolManager>();
builder.Services.AddScoped<IRedisClient, RedisClient>();

builder.Services.AddScoped<IActivityService, ActivityService.Services.Implementations.ActivityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
