namespace ActivityService.Services.Abstractions
{
    public interface IActivityService
    {
        Task PingClientAsync(Guid clientId);
    }
}
