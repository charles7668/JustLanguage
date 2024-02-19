using AutoMapper;
using JustLanguage.DTOs;
using JustLanguage.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustLanguage.DataBase;

public class ParseRuleRepository : IParseRuleRepository
{
    public ParseRuleRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public async Task AddParseRule(ParseRule parseRule)
    {
        _context.ParseRule.Add(parseRule);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public Task<bool> HasDuplicateName(ParseRule parseRule)
    {
        return _context.ParseRule.AnyAsync(x => x.Name == parseRule.Name);
    }

    /// <inheritdoc />
    public async Task<bool> HasDuplicateDomain(ParseRule parseRule)
    {
        foreach (SupportDomain ruleSupportDomain in parseRule.SupportDomains)
        {
            bool dup = await _context.SupportDomain.AnyAsync(x => x.Domain == ruleSupportDomain.Domain.Trim());
            if (dup)
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>> GetParseRulesNames()
    {
        return Task.FromResult<IEnumerable<string>>(_context.ParseRule.Select(x => x.Name));
    }

    /// <inheritdoc />
    public Task<ParseRule?> GetParseRuleByName(string name)
    {
        IQueryable<ParseRule> rules = _context.ParseRule.Where(x => x.Name == name).Include(x => x.SupportDomains);

        return rules.FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateParseRuleByName(string name, ParseRuleDTO newRule)
    {
        ParseRule? rule = await _context.ParseRule.Where(x => x.Name == name).Include(s => s.SupportDomains)
            .FirstOrDefaultAsync();
        if (rule == null)
        {
            return false;
        }

        _mapper.Map(newRule, rule);
        await _context.SaveChangesAsync();
        return true;
    }
}