using JustLanguage.Constants;
using JustLanguage.Interfaces;
using System.IO.Compression;

namespace JustLanguage.Services;

public class HttpCrawler : IHttpCrawler
{
    public HttpCrawler(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(HttpClientConstants.DEFAULT_HTTP_CLIENT_NAME);
    }

    private readonly HttpClient _httpClient;

    public async Task<string> GetAsync(Uri uri)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(uri);
        HttpContent content = response.Content;
        Stream stream;
        if (content.Headers.ContentEncoding.Contains("gzip"))
        {
            stream = new GZipStream(await content.ReadAsStreamAsync(), CompressionMode.Decompress);
        }
        else if (content.Headers.ContentEncoding.Contains("deflate"))
        {
            stream = new DeflateStream(await content.ReadAsStreamAsync(), CompressionMode.Decompress);
        }
        else
        {
            stream = await content.ReadAsStreamAsync();
        }

        using var sr = new StreamReader(stream);
        return await sr.ReadToEndAsync();
    }
}