using JustLanguage.Entities;

namespace JustLanguage.DataBase;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(AppDbContext context)
    {
        ParseRuleRepository = new ParseRuleRepository(context);
    }

    // repositories
    public IParseRuleRepository ParseRuleRepository { get; }
}