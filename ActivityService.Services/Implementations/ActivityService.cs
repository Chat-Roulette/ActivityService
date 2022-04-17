using ActivityService.Services.Abstractions;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace ActivityService.Services.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IRedisClient _redisClient;

        public ActivityService(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task PingClientAsync(Guid clientId)
        {
            var database = _redisClient.GetDb(0);
            var key = clientId.ToString();

            if (await database.ExistsAsync(key))
            {
                await database.UpdateExpiryAsync(key, DateTimeOffset.Now.AddSeconds(5));
            }
            else
            {
                await database.AddAsync(key, string.Empty,DateTimeOffset.Now.AddSeconds(5));
            }
        }
    }
}
