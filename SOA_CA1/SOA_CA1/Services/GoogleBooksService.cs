using System.Text.Json;
using RestSharp;
using SOA_CA1.Clients.Models;
using SOA_CA1.Services.Interfaces;
using System.Diagnostics;
using SOA_CA1;
using System.ComponentModel;

namespace SOA_CA1.Services
{
	public class GoogleBooksService :BaseWebService<GoogleBooksResponseModel>, IGoogleBooksService
	{
		public GoogleBooksResponseModel cacheBook { get; set; }
		public IEnumerable<Book> SearchedBooks { get; set; }
		public string SearchedText { get; set; }
		static readonly int maxBooks = 40;
		public int TotalBooksResult { get; set; }

		public GoogleBooksService(IConfiguration configuration):base(configuration, "GoogleBookAPI")
		{
		}

		public override async Task<GoogleBooksResponseModel> SearchAsync(string query)
		{
			var request = new RestRequest();
			request.AddParameter("q", query);
			request.AddParameter("key", _apiKey);
			request.AddParameter("maxResults", maxBooks);
			var response = await _client.ExecuteAsync(request);

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					GoogleBooksResponseModel books = JsonSerializer.Deserialize<GoogleBooksResponseModel>(response.Content);
                    SearchedBooks = books.Books;
					cacheBook = books; 
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

		public async Task<GoogleBooksResponseModel> SearchAsync(string query, int currentPage = 1)
		{
			var currentIndex = (currentPage - 1) * maxBooks;
            var request = new RestRequest();
            request.AddParameter("q", query);
            request.AddParameter("key", _apiKey);
			request.AddParameter("startIndex", currentIndex);
            request.AddParameter("maxResults", maxBooks);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
            {
                try
                {
                    GoogleBooksResponseModel books = JsonSerializer.Deserialize<GoogleBooksResponseModel>(response.Content);
                    SearchedBooks = books.Books;
                    cacheBook = books;
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

        public GoogleBooksResponseModel SortBooksAlphabet(bool ascending)
        {
			
            if (SearchedBooks == null || !SearchedBooks.Any())
            {
                return cacheBook;
            }

            IEnumerable<Book> sortedBooks = ascending
                ? SearchedBooks.Where(book => !string.IsNullOrEmpty(book.VolumeInfo?.Title))
                                .OrderBy(book => book.VolumeInfo.Title)
                : SearchedBooks.Where(book => !string.IsNullOrEmpty(book.VolumeInfo?.Title))
                                .OrderByDescending(book => book.VolumeInfo.Title);

            cacheBook.Books = sortedBooks.ToList();
            return cacheBook;
        }


        public GoogleBooksResponseModel SortBooksByDate(bool ascending)
        {
            if (SearchedBooks == null || !SearchedBooks.Any())
            {
                return cacheBook;
            }

			IEnumerable<Book> sortedBooks = ascending ? SearchedBooks.Where(book => !string.IsNullOrEmpty(book.VolumeInfo?.PublishedDate)).OrderBy(book => book.VolumeInfo.PublishedDate)
										:SearchedBooks.Where(book => !string.IsNullOrEmpty(book.VolumeInfo?.PublishedDate)).OrderByDescending(book => book.VolumeInfo.PublishedDate);

			cacheBook.Books = sortedBooks.ToList();
			return cacheBook;
		}

        public async Task<Book> GetBookByIdAsync(string id)
		{
			var request = new RestRequest($"/{id}");
            request.AddParameter("key", _apiKey);
            var response = await _client.ExecuteAsync(request);

			if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
			{
				try
				{
					Book book = JsonSerializer.Deserialize<Book>(response.Content);
					return book;
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
				return null;
			}
		}
	}
}
