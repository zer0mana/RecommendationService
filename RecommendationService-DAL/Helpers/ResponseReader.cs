using System.Text.Json;

namespace RecommendationService_DAL.Helpers;

public static class ResponseReader<T>
{
    public static async Task<T> ReadAsync(
        HttpResponseMessage? response,
        CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync(cancellationToken));
        }
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<T>(jsonResponse, options);

        return result;
    }
}