using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Movie : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? OriginalTitle { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string? PosterPath { get; set; }
        public string? BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string OriginalLanguage { get; set; }
        public int? ImdbId { get; set; }
        public string? Status { get; set; }
        public bool? NowPlaying { get; set; }
        public string? TrailerUrl { get; set; }

        public double Rating { get; set; }
        public string Director { get; set; }

        public ICollection<MovieGenre> Genres = new HashSet<MovieGenre>();
        public ICollection<MovieSession> MovieSessions = new HashSet<MovieSession>();
    }
}
