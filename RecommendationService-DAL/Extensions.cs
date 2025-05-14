using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using RecommendationService_DAL.Caches;
using RecommendationService_DAL.Caches.Interfaces;
using RecommendationService_DAL.Facades;
using RecommendationService_DAL.Facades.Interfaces;
using RecommendationService_DAL.Redis;

namespace RecommendationService_DAL;

public static class Extensions
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddSingleton<IUserNearestNeighboursCache, UserNearestNeighboursCache>();
        services.AddSingleton<IExternalFacade, ExternalFacade>();

        return services;
    }
}