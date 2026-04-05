using System.Text.RegularExpressions;

namespace Movies.Application.Models
{
    public class Movie
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string Slug { get; set; } = null!;
        public required int YearOfRelease { get; set; }
        public required List<string> Genres { get; set; } = new();
    }
}
