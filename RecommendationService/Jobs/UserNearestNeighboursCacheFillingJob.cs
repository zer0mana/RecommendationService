using RecommendationService_DAL.Caches.Interfaces;

namespace RecommendationService.Jobs;

public class UserNearestNeighboursCacheFillingJob : BackgroundService
{
    private readonly IUserNearestNeighboursCache _userNearestNeighboursCache;

    public UserNearestNeighboursCacheFillingJob(IUserNearestNeighboursCache userNearestNeighboursCache)
    {
        _userNearestNeighboursCache = userNearestNeighboursCache;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _userNearestNeighboursCache.RefreshAsync(stoppingToken);
                
                await timer.WaitForNextTickAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}