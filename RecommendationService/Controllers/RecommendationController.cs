using Microsoft.AspNetCore.Mvc;
using RecommendationService_BLL.Services.Interfaces;

namespace RecommendationService.Controllers;

[ApiController]
[Route("recommendation-service/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }
    
    [HttpGet("recommendations/{userId}")]
    public async Task<IActionResult> GeUserRecommendations(long userId)
    {
        var taskListIds = await _recommendationService.GetUserRecommendationAsync(
            userId,
            CancellationToken.None);
        
        return Ok(taskListIds);
    }
}