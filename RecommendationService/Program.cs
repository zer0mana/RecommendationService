using FluentValidation.AspNetCore;
using RecommendationService_BLL;
using RecommendationService_DAL;
using RecommendationService_DAL.Redis;
using RecommendationService.Jobs;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = true;
});

builder.Services.AddBll();
builder.Services.AddDal();

builder.Services.AddHostedService<UserNearestNeighboursCacheFillingJob>();

builder.Services.AddSingleton<IRedisRepository, RedisRepository>(sp =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379", true);
    return new RedisRepository(configuration);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3007", "http://localhost:3009", "http://localhost:3004")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "UserAPI v1");
});

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();