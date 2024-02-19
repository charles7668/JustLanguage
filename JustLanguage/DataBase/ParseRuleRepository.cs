using JustLanguage.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustLanguage.DataBase;

public class ParseRuleRepository : IParseRuleRepository
{
    public ParseRuleRepository(AppDbContext context)
    {
        _context = context;
    }

    private readonly AppDbContext _context;

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
}