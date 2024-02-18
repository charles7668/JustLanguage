using JustLanguage.Entities;

namespace JustLanguage.DataBase;

public interface IParseRuleRepository
{
    /// <summary>
    /// Add a parse rule to the database
    /// </summary>
    /// <param name="parseRule"></param>
    /// <returns></returns>
    public Task AddParseRule(ParseRule parseRule);
}