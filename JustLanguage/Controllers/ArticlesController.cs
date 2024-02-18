using JustLanguage.DataBase;
using JustLanguage.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JustLanguage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : Controller
{
    public ArticlesController(ILogger<ArticlesController> logger, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private readonly ILogger<ArticlesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    [HttpPost]
    public Task<IActionResult> Post([FromBody] UploadArticleDTO dto)
    {
        _logger.LogInformation("input : {@DTO}", dto);
        // TODO using XPath to parse article

        return Task.FromResult<IActionResult>(Ok());
    }
}