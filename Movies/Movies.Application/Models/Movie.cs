using System.Text.RegularExpressions;

namespace Movies.Application.Models
{
    public class Movie
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string Slug { get; set; } = null!;
        public required int YearOfRelease { get; set; }
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }

        public List<Rating> Ratings { get; set; } = new ();
        public required List<string> Genres { get; set; } = new();

    }
}
