using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jmwoo.Core.Http;
using Microsoft.Extensions.Configuration;

namespace Assistant.Features.News
{
    public interface INewsService
    {
        Task<string> GetHeadlinesMessage();
        Task<HeadlinesResult> GetHeadlines();
    }

    public class NewsService : INewsService
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly string _apiKey;
        readonly IHeadlinesCache _headlinesCache;

        private const string NewsApiConfigKey = "Features:News:NewsApiKey";

        public NewsService(
            IHttpClientFactory httpClientFactory, IConfiguration config, IHeadlinesCache headlinesCache
        )
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = config[NewsApiConfigKey];

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception($"No NewsApi Key, place key in {NewsApiConfigKey}");
            }

            _headlinesCache = headlinesCache;
        }

        public async Task<HeadlinesResult> GetHeadlines()
        {
            var result = await _headlinesCache.Get();

            if (result == null)
            {
                result = await this.GetHeadlinesFromApi();
                await _headlinesCache.Set(result);
            }

            return result;
        }

        private async Task<HeadlinesResult> GetHeadlinesFromApi()
        {
            var p = new Dictionary<string, string>
            {
                { "apiKey", _apiKey },
                { "sources", AcceptableSourcesStr }
            };

            var url = HttpHelpers.BuildUrl("newsapi.org", "v2/top-headlines", queryParams: p, useHttps: true);
            var result = await _httpClientFactory.CreateClient().SendAndReceiveAs<HeadlinesApiResult>(HttpMethod.Get, url);

            var headlines = result.Articles.Select(x => new Headline
            {
                SourceName = x.Source.Name,
                Title = x.Title,
            }).ToList();

            return new HeadlinesResult
            {
                Headlines = headlines
            };
        }

        public async Task<string> GetHeadlinesMessage()
        {
            var headlinesResult = await this.GetHeadlines();
            var sb = new StringBuilder("Here are today's top headlines: ");
            var headlineIndex = 0;
            foreach (var headline in headlinesResult.Headlines.Take(5))
            {
                headlineIndex++;
                sb.Append($"({headlineIndex}). From {headline.SourceName}, {headline.Title}. ");
            }

            return sb.ToString();
        }

        private string[] AcceptableSources => new string[] {
            //"abc-news",
            //"ars-technica",
            "the-new-york-times",
            "associated-press",
            //"axios",
            //"bloomberg",
            //"breitbart-news",
            //"business-insider",
            //"cbs-news",
            "cnn",
            "fox-news",
            //"politico",
            //"reuters",
            //"techcrunch",
            "the-hill",
            "the-wall-street-journal",
            "the-washington-post",
        };

        private string AcceptableSourcesStr => string.Join(",", this.AcceptableSources);
    }
}