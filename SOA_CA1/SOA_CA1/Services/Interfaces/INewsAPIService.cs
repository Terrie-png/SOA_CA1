using SOA_CA1.Clients.Models;

namespace SOA_CA1.Services.Interfaces
{
	public interface INewsAPIService
	{
		public Task<NewsApiResponseModel> SearchAsync(string query);
	}
}
