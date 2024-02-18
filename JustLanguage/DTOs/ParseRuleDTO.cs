namespace JustLanguage.DTOs;

public class ParseRuleDTO
{
    public string Name { get; set; } = string.Empty;
    public string Rule { get; set; } = string.Empty;
    public IEnumerable<SupportDomainDTO> SupportDomains { get; set; } = new List<SupportDomainDTO>();
}