using System.Text.Json;
using HtmlAgilityPack;
using JustLanguage.Constants;
using JustLanguage.DataBase;
using JustLanguage.DTOs;
using JustLanguage.Entities;
using JustLanguage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JustLanguage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : Controller
{
    public ArticlesController(ILogger<ArticlesController> logger, IUnitOfWork unitOfWork, IHttpCrawler crawler)
    {
        _parseRuleRepository = unitOfWork.ParseRuleRepository;
        _articleInfoRepository = unitOfWork.ArticleInfoRepository;
        _logger = logger;
        _httpCrawler = crawler;
    }

    private readonly IArticleInfoRepository _articleInfoRepository;

    /// <summary>
    /// default http crawler
    /// </summary>
    private readonly IHttpCrawler _httpCrawler;

    private readonly ILogger<ArticlesController> _logger;
    private readonly IParseRuleRepository _parseRuleRepository;

    [HttpPost]
    public async Task<ActionResult<ArticleInfoDTO>> UploadArticle([FromBody] UploadArticleDTO dto)
    {
        _logger.LogInformation("input : {@DTO}", dto);
        ParseRule? parseRule = await _parseRuleRepository.GetParseRuleBySupportDomain(dto.ArticleUrl);
        if (parseRule == null)
        {
            return BadRequest("no parse rule for this domain");
        }

        string response = await _httpCrawler.GetAsync(new Uri(dto.ArticleUrl));
        var document = new HtmlDocument();
        document.LoadHtml(response);
        HtmlNode? node = document.DocumentNode;
        var originUri = new Uri(dto.ArticleUrl);
        var articleInfoDto = new ArticleInfoDTO
        {
            SrcUrl = originUri.AbsoluteUri
        };
        if (node == null)
        {
            return Ok(articleInfoDto);
        }

        Dictionary<string, string> ruleDict = JsonSerializer.Deserialize<Dictionary<string, string>>(parseRule.Rule) ??
                                              new Dictionary<string, string>();
        string titleRuleKey = ParseRuleConstants.DEFAULT_PARSE_TITLE_XPATH;
        string coverRuleKey = ParseRuleConstants.DEFAULT_PARSE_COVER_XPATH;
        string articleRuleKey = ParseRuleConstants.DEFAULT_PARSE_ARTICLE_XPATH;
        string authorRuleKey = ParseRuleConstants.DEFAULT_PARSE_AUTHOR_XPATH;
        articleInfoDto.Title = TryToParse(titleRuleKey, ExtractInnerText);
        articleInfoDto.Content = TryToParse(articleRuleKey, ExtractInnerText);
        articleInfoDto.Author = TryToParse(authorRuleKey, ExtractInnerText);
        articleInfoDto.CoverImageBase64 = TryToParse(coverRuleKey, ExtractImageSrc);

        bool state = await _articleInfoRepository.AddArticle(articleInfoDto);
        if (!state)
        {
            return BadRequest("add article to database failed");
        }

        return Ok(articleInfoDto);

        string ExtractInnerText(HtmlNode? targetNode)
        {
            return targetNode != null ? targetNode.InnerText : "";
        }

        string ExtractImageSrc(HtmlNode? targetNode)
        {
            string? src = targetNode?.Attributes["src"]?.Value;
            if (src == null)
            {
                return "";
            }

            bool isAbsoluteUri = Uri.TryCreate(src, UriKind.Absolute, out Uri? srcUri);
            if (isAbsoluteUri)
            {
                return srcUri!.AbsoluteUri;
            }

            var completeUri = new Uri(originUri, src);
            return completeUri.AbsoluteUri;
        }

        string TryToParse(string keyName, Func<HtmlNode?, string> extractFunction)
        {
            _logger.LogInformation("try to parse {KeyName}", keyName);
            if (!ruleDict.TryGetValue(keyName, out string? value))
            {
                return "";
            }

            HtmlNode? targetNode = null;
            try
            {
                targetNode = node.SelectSingleNode(value);
            }
            catch (Exception e)
            {
                _logger.LogTrace("parse {KeyName} fail : {Error}", keyName, e.Message);
            }

            _logger.LogInformation("parse {KeyName} success", keyName);

            return extractFunction(targetNode);
        }
    }

    [HttpGet(Name = "{page}")]
    public async Task<ActionResult<IEnumerable<ArticleInfoDTO>>> GetArticles([FromQuery] int page = 0)
    {
        IEnumerable<ArticleInfoDTO> articleDtoList = await _articleInfoRepository.GetArticles(page);
        return Ok(articleDtoList);
    }
}