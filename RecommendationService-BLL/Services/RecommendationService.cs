using RecommendationService_BLL.Services.Interfaces;
using RecommendationService_DAL.Caches.Interfaces;

namespace RecommendationService_BLL.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IUserNearestNeighboursCache _userNearestNeighboursCache;
 
    public RecommendationService(IUserNearestNeighboursCache userNearestNeighboursCache)
    {
        _userNearestNeighboursCache = userNearestNeighboursCache;
    }
 
    public async Task<List<long>> GetUserRecommendationAsync(long userId, CancellationToken cancellationToken)
    {
        var recommendations = new List<long>();
 
        recommendations.AddRange(await RecommendByNearestNeighbourAsync(userId, cancellationToken));
        // recommendations.AddRange(await RecommendByMostPopularToDoListsAsync(userId, cancellationToken));
        // recommendations.AddRange(await RecommendByNewestToDoListsAsync(userId, cancellationToken));
 
        recommendations = recommendations.Distinct().ToList();
 
        return recommendations;
    }
 
    private async Task<List<long>> RecommendByNearestNeighbourAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var neighbours = await _userNearestNeighboursCache.GetUserNearestNeighboursAsync(
            userId, 
            cancellationToken);
 
        return neighbours;
    }
}