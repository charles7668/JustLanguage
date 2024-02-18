using JustLanguage.Entities;

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
}