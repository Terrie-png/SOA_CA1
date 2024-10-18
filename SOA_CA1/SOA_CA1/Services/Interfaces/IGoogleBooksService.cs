using SOA_CA1.Clients.Models;

namespace SOA_CA1.Services.Interfaces
{
    public interface IGoogleBooksService
	{

		public Task<GoogleBooksResponseModel> SearchAsync(string query);
        public Task<GoogleBooksResponseModel> SearchAsync(string query, int currentPage = 1);

        public Task<Book> GetBookByIdAsync(string id);

        public GoogleBooksResponseModel SortBooksAlphabet(bool ascending);
        public GoogleBooksResponseModel SortBooksByDate(bool ascending);


    }
}
