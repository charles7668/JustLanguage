namespace JustLanguage.DataBase;

public interface IUnitOfWork
{
    IParseRuleRepository ParseRuleRepository { get; }
    IArticleInfoRepository ArticleInfoRepository { get; }
}