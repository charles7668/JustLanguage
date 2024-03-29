﻿using JustLanguage.DTOs;
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

    /// <summary>
    /// get parse rule by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<ParseRule?> GetParseRuleByName(string name);

    /// <summary>
    /// update parse rule by name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="newRule"></param>
    /// <returns></returns>
    public Task<bool> UpdateParseRuleByName(string name, ParseRuleDTO newRule);

    /// <summary>
    /// get parse rule by support domain
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public Task<ParseRule?> GetParseRuleBySupportDomain(string url);

    /// <summary>
    /// delete parse rule by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task DeleteParseRuleByName(string name);
}