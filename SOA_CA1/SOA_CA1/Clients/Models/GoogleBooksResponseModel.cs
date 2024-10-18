using System.Text.Json.Serialization;

namespace SOA_CA1.Clients.Models
{
    public class GoogleBooksResponseModel
    {
        [JsonPropertyName("kind")]
        public string? BooksKind { get; set; }
        [JsonPropertyName("totalItems")]
        public int? TotalItems { get; set; }
        [JsonPropertyName("items")]
        public List<Book> Books { get; set; }

    }


}

