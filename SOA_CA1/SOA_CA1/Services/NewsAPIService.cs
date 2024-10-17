using System.Diagnostics;
using RestSharp;
using SOA_CA1.Clients.Models;
using System.Text.Json;
using SOA_CA1.Services.Interfaces;
namespace SOA_CA1.Services
{
	public class NewsAPIService :BaseWebService<NewsApiResponseModel>, INewsAPIService
	{ 
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
