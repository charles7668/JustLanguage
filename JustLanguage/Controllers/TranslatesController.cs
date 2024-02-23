using System.Collections.Specialized;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;
using JustLanguage.Constants;
using JustLanguage.DTOs;
using JustLanguage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JustLanguage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslatesController : ControllerBase
{
    public TranslatesController(IHttpCrawler httpCrawler, ILogger<TranslatesController> logger)
    {
        _httpCrawler = httpCrawler;
        _logger = logger;
    }

    private readonly IHttpCrawler _httpCrawler;
    private readonly ILogger<TranslatesController> _logger;

    [HttpPost]
    public async Task<IActionResult> Translate([FromBody] TranslateOptionDto dto)
    {
        if (dto.TranslateProvider == TranslateProvider.GOOGLE)
        {
            var uriBuilder =
                new UriBuilder(
                    "https://translate.google.com/translate_a/single?dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t");
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(dto.Query);
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (jsonObject != null)
            {
                IEnumerable<KeyValuePair<string, JsonNode?>> enumerable = jsonObject.AsEnumerable();
                foreach (KeyValuePair<string, JsonNode?> pair in enumerable)
                {
                    query[pair.Key] = pair.Value?.ToString();
                }
            }

            uriBuilder.Query = query.ToString();
            _logger.LogInformation("Translator URL : {URL}", uriBuilder.Uri);
            string response = await _httpCrawler.GetAsync(uriBuilder.Uri);
            var responseJson = JsonSerializer.Deserialize<JsonArray>(response);
            string result = "";
            // if response is null or empty , then return empty string
            if (responseJson == null || responseJson.Count < 1)
            {
                return Ok(result);
            }

            // resolve google response format
            JsonArray? arr1 = responseJson[0]?.AsArray();
            if (arr1 == null)
            {
                return Ok(result);
            }

            result = arr1.Aggregate(result, (current, jsonNode) => current + jsonNode?[0]?.ToString().Trim());


            return Ok(result);
        }

        return BadRequest("not support translate provider");
    }
}