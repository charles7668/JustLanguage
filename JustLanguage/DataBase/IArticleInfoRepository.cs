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

    /// <summary>
    /// get article info with offset and limit
    /// </summary>
    /// <param name="offset">offset to get articles</param>
    /// <param name="limit">limit of articles count</param>
    /// <returns></returns>
    Task<IEnumerable<ArticleInfoDTO>> GetArticles(int offset = 0, int limit = 20);
}