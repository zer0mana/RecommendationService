namespace RecommendationService_BLL.Services;

public interface IRecommendationService
{
    Task<List<long>> GetUserRecommendationAsync(
        long userId,
        CancellationToken cancellationToken);
}