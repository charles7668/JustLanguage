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
    public async Task<ActionResult> SetParseRules([FromBody] ParseRuleDTO ruleDto)
    {
        var rule = _mapper.Map<ParseRule>(ruleDto);
        if (await _parseRuleRepository.HasDuplicateName(rule))
        {
            _logger.LogError("the name is exists");
            return BadRequest("the name is exists");
        }

        if (await _parseRuleRepository.HasDuplicateDomain(rule))
        {
            _logger.LogError("the domain is exists");
            return BadRequest("the domain is exists");
        }

        _logger.LogInformation("Rule Name : {Name} , Rule : {Rule}", rule.Name, rule.Rule);
        await _parseRuleRepository.AddParseRule(rule);
        return Ok();
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<ParseRuleDTO>> GetParseRule(string name)
    {
        ParseRule? rule = await _parseRuleRepository.GetParseRuleByName(name);
        if (rule == null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<ParseRuleDTO>(rule);

        return Ok(dto);
    }
}