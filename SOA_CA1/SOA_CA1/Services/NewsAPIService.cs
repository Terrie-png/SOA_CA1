using System.Diagnostics;
using RestSharp;
using SOA_CA1.Clients.Models;
using System.Text.Json;
namespace SOA_CA1.Services
{
	public class NewsAPIService :BaseWebService
	{
		private static readonly string google_url = "https://newsapi.org/v2/everything?apiKey=e0e82d634a254df98f89d7596a91df3e&sortBy=popularity";
		private readonly RestClient _client;

		public NewsAPIService()
		{
			_client = new RestClient(google_url);
		}

		public async Task<NewsApiResponseModel> SearchNewsAsync(string query)
		{
			var request = new RestRequest();
			request.AddParameter("q", query);
			var response = await _client.ExecuteAsync(request);

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					NewsApiResponseModel news = JsonSerializer.Deserialize<NewsApiResponseModel>(response.Content);
					return news;
				}
				catch (JsonException ex)
				{
					// Log or handle the deserialization error
					Debug.WriteLine($"Deserialization error: {ex.Message}");
					throw new InvalidOperationException("Failed to parse news search response.", ex);
				}
			}
			else
			{
				throw new InvalidOperationException($"Failed to retrieve news: {response.ErrorMessage}");
			}
		}
	}
}
