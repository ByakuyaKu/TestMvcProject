using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Anime
    {
        //main fields
        #region
        //api fields
        #region
        public string? Duration { get; set; }
        public int? Favorites { get; set; }
        public double? Score { get; set; }
        public int? ScoredBy { get; set; }
        public string? TitleJapanese { get; set; }
        public int? Volumes { get; set; }
        public string? LinkCanonical { get; set; }
        public int? Popularity { get; set; }
        public string? Premiered { get; set; }
        public int? Rank { get; set; }
        public string? Rating { get; set; }
        public string? Source { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        #endregion
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(AllowEmptyStrings = false)]
        public string Tittle { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;
        public DateTime? AnimeStarts { get; set; }
        public DateTime? AnimeEnds { get; set; }
        [Range(0, int.MaxValue)]
        public int? SeriesCount { get; set; }
        [Range(0, int.MaxValue)]
        public int? SeriesRealesed { get; set; }
        public string? Description { get; set; }
        #endregion

        //foreign keys
        #region
        public List<Genre>? Genres { get; set; } = new List<Genre>();

        public List<Manga>? Manga { get; set; } = new List<Manga>();

        public List<Author>? Authors { get; set; } = new List<Author>();

        public List<Image>? Images { get; set; } = new List<Image>();

        //public List<Position>? Positions { get; set; } = new List<Position>();
        #endregion

        //NotMapped fields
        #region
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        [NotMapped]
        public List<Guid>? MangaIdList { get; set; } = new List<Guid>();
        [NotMapped]
        public List<Guid>? AuthorIdList { get; set; } = new List<Guid>();
        [NotMapped]
        public List<Guid>? GenreIdList { get; set; } = new List<Guid>();
        #endregion

    }
}
