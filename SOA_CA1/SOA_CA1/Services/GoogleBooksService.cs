using System.Text.Json;
using RestSharp;
using SOA_CA1.Clients.Models;
using SOA_CA1.Services.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SOA_CA1.Services
{
	public class GoogleBooksService : BaseWebService, IGoogleBooksService
	{
		private static readonly string google_url = "https://www.googleapis.com/books/v1/volumes?";
		private readonly RestClient _client;

		public GoogleBooksService()
		{
			_client = new RestClient(google_url);
		}

		public async Task<Book> SearchBooksAsync(string query)
		{
			var request = new RestRequest();
			request.AddParameter("q", query);
			var response = await _client.ExecuteAsync(request);

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					GoogleBooksResponseModel books = JsonSerializer.Deserialize<Book>(response.Content);
					return books;
				}
				catch (JsonException ex)
				{
					// Log or handle the deserialization error
					Debug.WriteLine($"Deserialization error: {ex.Message}");
					throw new InvalidOperationException("Failed to parse book search response.", ex);
				}
			}
			else
			{
				throw new InvalidOperationException($"Failed to retrieve books: {response.ErrorMessage}");
			}
		}

		public async Task<GoogleBooksResponseModel> GetBookByIdAsync(string id)
		{
			var request = new RestRequest($"/{id}");

			var response = await _client.ExecuteAsync(request);

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					return JsonSerializer.Deserialize<GoogleBooksResponseModel>(response.Content);
				}
				catch (JsonException ex)
				{
					// Log or handle the deserialization error
					Debug.WriteLine($"Deserialization error: {ex.Message}");
					throw new InvalidOperationException("Failed to parse book details response.", ex);
				}
			}
			else
			{
				throw new InvalidOperationException($"Failed to retrieve book details: {response.ErrorMessage}");
			}
		}
	}
}
