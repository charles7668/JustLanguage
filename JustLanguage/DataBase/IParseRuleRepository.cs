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

    /// <summary>
    /// check if the parse rule has a duplicate name
    /// </summary>
    /// <param name="parseRule"></param>
    /// <returns></returns>
    public Task<bool> HasDuplicateName(ParseRule parseRule);

    /// <summary>
    /// check if the parse rule has a duplicate domain
    /// </summary>
    /// <param name="parseRule"></param>
    /// <returns></returns>
    public Task<bool> HasDuplicateDomain(ParseRule parseRule);

    /// <summary>
    /// get all parse rules name
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<string>> GetParseRulesNames();
}