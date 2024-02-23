namespace JustLanguage.DTOs;

public class TranslateOptionDto
{
    /// <summary>
    /// translate provider option
    /// </summary>
    public string TranslateProvider { get; set; } = String.Empty;

    /// <summary>
    /// query json string
    /// </summary>
    public string Query { get; set; } = String.Empty;
}