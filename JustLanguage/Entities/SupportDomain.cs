namespace JustLanguage.Entities;

public class SupportDomain
{
    public int Id { get; set; }

    /// <summary>
    /// Domain URL
    /// </summary>
    public string Domain { get; set; } = string.Empty;

    // relations
    public int ParseRuleId { get; set; }
    public ParseRule ParseRule { get; set; } = null!;
}