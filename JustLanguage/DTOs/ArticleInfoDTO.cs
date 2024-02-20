namespace JustLanguage.DTOs;

public class ArticleInfoDTO
{
    /// <summary>
    /// article title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// article content
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// article cover image base64 string
    /// </summary>
    public string CoverImageBase64 { get; set; }

    /// <summary>
    /// article author
    /// </summary>
    public string Author { get; set; }
}