using ActivityService.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace ActivityService.Services.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IRedisClient _redisClient;
        private readonly int _clientTimeout;

        public ActivityService(IRedisClient redisClient, IConfiguration configuration)
        {
            _redisClient = redisClient;
            _clientTimeout = int.Parse(configuration.GetRequiredSection("ClientTimeout").Value);
        }

        public async Task PingClientAsync(Guid clientId)
        {
            var database = _redisClient.GetDb(0);
            var key = clientId.ToString();

            if (await database.ExistsAsync(key))
            {
                await database.UpdateExpiryAsync(key, DateTimeOffset.Now.AddSeconds(_clientTimeout));
            }
            else
            {
                await database.AddAsync(key, string.Empty, DateTimeOffset.Now.AddSeconds(_clientTimeout));
            }
        }
    }
}
