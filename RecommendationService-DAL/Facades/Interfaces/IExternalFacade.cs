namespace RecommendationService_DAL.Facades.Interfaces;

public interface IExternalFacade
{
    Task<List<long>> GetUserNearestNeighboursAsync(
        long userId,
        CancellationToken cancellationToken);
}