using System.ComponentModel.DataAnnotations;

namespace JustLanguage.DTOs;

public class UploadArticleDTO
{
    [Url(ErrorMessage = "article url is not url format")]
    public string ArticleUrl { get; set; } = string.Empty;
}