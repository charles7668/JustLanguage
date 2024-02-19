using JustLanguage.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace JustLanguage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParseRuleNamesController : Controller
{
    public ParseRuleNamesController(ILogger<ParseRulesController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _parseRuleRepository = unitOfWork.ParseRuleRepository;
    }

    private readonly ILogger<ParseRulesController> _logger;
    private readonly IParseRuleRepository _parseRuleRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetParseRulesNames()
    {
        IEnumerable<string> names = await _parseRuleRepository.GetParseRulesNames();
        _logger.LogInformation("GetParseRulesNames Count : {Count}", names.Count());
        return Ok(names);
    }
}