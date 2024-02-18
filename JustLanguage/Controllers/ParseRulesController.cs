using AutoMapper;
using JustLanguage.DataBase;
using JustLanguage.DTOs;
using JustLanguage.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JustLanguage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParseRulesController : Controller
{
    public ParseRulesController(ILogger<ParseRulesController> logger, IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _parseRuleRepository = unitOfWork.ParseRuleRepository;
    }

    private readonly ILogger<ParseRulesController> _logger;
    private readonly IMapper _mapper;
    private readonly IParseRuleRepository _parseRuleRepository;

    [HttpPost]
    public async Task<IActionResult> SetParseRules([FromBody] ParseRuleDTO ruleDto)
    {
        var rule = _mapper.Map<ParseRule>(ruleDto);
        _logger.LogInformation("Rule Name : {Name} , Rule : {Rule}", rule.Name, rule.Rule);
        await _parseRuleRepository.AddParseRule(rule);
        return Ok();
    }
}