using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text.Json;
using RestSharp;
using SOA_CA1.Clients.Models;

namespace SOA_CA1.Services
{
    public class GoogleBooksService
    {
        private static readonly string google_url = "https://www.googleapis.com/books/v1/volumes";
        private readonly RestClient _client;

        public GoogleBooksService()
        {
            _client = new RestClient(google_url);

        }

        public async Task<GoogleBooksResponseModel> SearchBooksAsync(string query)
        {
            var request = new RestRequest();
            request.AddParameter("q", query);
            var response =await _client.ExecuteAsync(request);
            if (!string.IsNullOrWhiteSpace(response.Content))
            {
                var serializer = JsonSerializer.Deserialize<GoogleBooksResponseModel>(response.Content);
                return serializer;
            }
            else
            {
                throw new Exception($"Failed to retrieve books: {response.ErrorMessage}");
            }
			
		}

		public async Task<GoogleBooksResponseModel> GetBookByIdAsync(string id)
		{
			var request = new RestRequest($"/{id}");

			var response = await _client.ExecuteAsync(request);
			if (response.IsSuccessful)
			{
                return JsonSerializer.Deserialize<GoogleBooksResponseModel>(response.Content);
			}
			else
			{
				throw new Exception($"Failed to retrieve book details: {response.ErrorMessage}");
			}
		}
	}
}
