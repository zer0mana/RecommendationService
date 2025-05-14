using RecommendationService_DAL.Facades.Interfaces;
using RecommendationService_DAL.Helpers;

namespace RecommendationService_DAL.Facades;

public class ExternalFacade : IExternalFacade
{
    private static readonly HttpClient client = new HttpClient();
    
    public async Task<List<long>> GetUserNearestNeighboursAsync(long userId, CancellationToken cancellationToken)
    {
        var url = $"http://pyd-service-app/pyd-service/ToDoTaskInternal/neighbours/{userId}";

        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await client.SendAsync(request);

        return await ResponseReader<List<long>>.ReadAsync(response, cancellationToken);
    }
}