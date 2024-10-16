using System.Text.Json.Serialization;

namespace SOA_CA1.Clients.Models
{
    public class NewsApiResponseModel
    {
		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonPropertyName("totalResults")]
		public int TotalResults { get; set; }

		[JsonPropertyName("articles")]
		public List<Article>? Article { get; set; }
	}

	public class Article
	{
		[JsonPropertyName("source")]
		public Source Source { get; set; }

		[JsonPropertyName("author")]
		public string Author { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }

		[JsonPropertyName("urlToImage")]
		public string UrlToImage { get; set; }

		[JsonPropertyName("publishedAt")]
		public DateTime PublishedAt { get; set; }

		[JsonPropertyName("content")]
		public string Content { get; set; }
	}

	public class Source
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
