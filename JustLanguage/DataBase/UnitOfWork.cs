using AutoMapper;
using JustLanguage.Entities;

namespace JustLanguage.DataBase;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        ParseRuleRepository = new ParseRuleRepository(context, mapper);
    }

    // repositories
    public IParseRuleRepository ParseRuleRepository { get; }
}