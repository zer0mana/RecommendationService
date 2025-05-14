using Microsoft.Extensions.DependencyInjection;
using RecommendationService_BLL.Services;
using RecommendationService_BLL.Services.Interfaces;

namespace RecommendationService_BLL;

public static class Extensions
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddSingleton<IRecommendationService, RecommendationService>();

        return services;
    }
}