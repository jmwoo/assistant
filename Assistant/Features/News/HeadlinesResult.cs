using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assistant.Features.News
{
    public class HeadlinesResult
    {
        public List<Headline> Headlines { get; set; }
    }

    public class Headline
    {
        public string SourceName { get; set; }
        public string Title { get; set; }
    }

    public class HeadlinesApiResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("articles")]
        public Article[] Articles { get; set; }
    }

    public class Article
    {
        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("urlToImage")]
        public Uri UrlToImage { get; set; }

        [JsonProperty("publishedAt")]
        public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public class Source
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
