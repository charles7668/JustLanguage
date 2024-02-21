using JustLanguage.DTOs;

namespace JustLanguage.DataBase;

public interface IArticleInfoRepository
{
    /// <summary>
    /// add an article to the database
    /// </summary>
    /// <param name="articleInfoDto"></param>
    /// <returns></returns>
    Task<bool> AddArticle(ArticleInfoDTO articleInfoDto);
}