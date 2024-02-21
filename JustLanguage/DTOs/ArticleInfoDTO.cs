namespace JustLanguage.DTOs;

public record ArticleInfoDTO
{
    /// <summary>
    /// article title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// article content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// article cover image base64 string
    /// </summary>
    public string CoverImageBase64 { get; set; } = string.Empty;

    /// <summary>
    /// article author
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// source url
    /// </summary>
    public string SrcUrl { get; set; } = string.Empty;
}