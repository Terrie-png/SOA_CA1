using System.Diagnostics;
using RestSharp;
using SOA_CA1.Clients.Models;
using System.Text.Json;
using SOA_CA1.Services.Interfaces;
namespace SOA_CA1.Services
{
	public class NewsAPIService :BaseWebService<NewsApiResponseModel>, INewsAPIService
	{ 
		private NewsApiResponseModel cache_news { get; set; } = new NewsApiResponseModel();
		public NewsAPIService(IConfiguration configuration):base(configuration, "NewsAPI")
		{
		}

		public override async Task<NewsApiResponseModel> SearchAsync(string query)
		{
			var request = new RestRequest();
			request.AddParameter("q", query);
			request.AddParameter("apiKey", _apiKey);
			var response = await _client.ExecuteAsync(request);

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					NewsApiResponseModel news = JsonSerializer.Deserialize<NewsApiResponseModel>(response.Content);
                    cache_news = news;
					return cache_news;
				}
				catch (JsonException ex)
				{
					// Log or handle the deserialization error
					Debug.WriteLine($"Deserialization error: {ex.Message}");
                    Console.WriteLine(new InvalidOperationException("Failed to parse news search response.", ex));
					return null;

                }
			}
			else
			{
                Console.WriteLine(new InvalidOperationException($"Failed to retrieve news: {response.ErrorMessage}"));
				return null;

            }
		}
	}
}
