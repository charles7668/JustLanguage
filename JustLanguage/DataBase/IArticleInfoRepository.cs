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
    /// get article info with page and limit
    /// </summary>
    /// <param name="page">page to get articles</param>
    /// <param name="limit">limit of articles count</param>
    /// <param name="isDescending"></param>
    /// <returns></returns>
    Task<IEnumerable<ArticleInfoDTO>> GetArticles(int page = 0, int limit = 20, bool isDescending = true);

    /// <summary>
    /// check if the article is duplicate or not
    /// </summary>
    /// <param name="uploadArticleDto"></param>
    /// <returns></returns>
    Task<bool> HasDuplicateArticle(UploadArticleDTO uploadArticleDto);

    /// <summary>
    /// get article by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ArticleInfoDTO?> GetArticle(int id);

    /// <summary>
    /// delete article by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteArticleById(int id);

    /// <summary>
    /// get article count
    /// </summary>
    /// <returns></returns>
    Task<int> GetArticleCount();
}