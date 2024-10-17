using Microsoft.Extensions.Primitives;
using RestSharp;

namespace SOA_CA1.Services
{
	public abstract class BaseWebService<T>
	{
		protected IConfiguration _configuration;
		protected RestClient _client;
		protected string _apiKey { get; set; }
		protected string _baseUrl { get; set; }
		private string childName;

		protected BaseWebService(IConfiguration configuration, string configSection)
		{
			_configuration = configuration;


			_apiKey = _configuration[$"{configSection}:ApiKey"];
			_baseUrl = _configuration[$"{configSection}:Base_Url"];
			try
			{
				_client = new RestClient(_baseUrl);
			}
			catch
			{
				throw new ArgumentNullException("_baseUrl is empty please check the appsettings.json");
			}

			childName = configSection;

		}


		public abstract Task<T> SearchAsync(string query);

		public void printConfig()
		{
			Console.WriteLine("==========================================================================");
            Console.WriteLine($"Api key for {childName}.");
            Console.WriteLine(_apiKey);
            Console.WriteLine(_baseUrl);
			Console.WriteLine("==========================================================================");
		}
	}
}
