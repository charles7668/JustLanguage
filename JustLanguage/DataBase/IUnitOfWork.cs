namespace JustLanguage.DataBase;

public interface IUnitOfWork
{
    IParseRuleRepository ParseRuleRepository { get; }
}