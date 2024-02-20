namespace JustLanguage.Interfaces;

public interface IHttpCrawler
{
    /// <summary>
    /// get html content from uri
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    Task<string> GetAsync(Uri uri);
}