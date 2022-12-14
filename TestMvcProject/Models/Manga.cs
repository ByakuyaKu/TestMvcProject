using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Manga
    {

        //main fields
        #region
        //api fields
        #region
        public int? Favorites { get; set; }
        public decimal? Score { get; set; }
        public int? ScoredBy { get; set; }
        public string? TitleJapanese { get; set; }
        public int? Volumes { get; set; }
        public int? Popularity { get; set; }
        public int? Rank { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        #endregion

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(AllowEmptyStrings = false)]
        public string Tittle { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;
        public DateTime? MangaStarts { get; set; }
        public DateTime? MangaEnds { get; set; }
        public int? ChaptersCount { get; set; }
        public int? ChaptersRealesed { get; set; }
        public string? Description { get; set; }
        #endregion

        //foreign keys
        #region
        public List<Genre>? Genres { get; set; } = new List<Genre>();

        public List<Anime>? Anime { get; set; } = new List<Anime>();

        public List<Author>? Authors { get; set; } = new List<Author>();

        public List<Image>? Images { get; set; } = new List<Image>();
        #endregion

        //NotMapped fields
        #region
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        [NotMapped]
        public List<Guid>? AnimeIdList { get; set; } = new List<Guid>();
        [NotMapped]
        public List<Guid>? AuthorIdList { get; set; } = new List<Guid>();
        [NotMapped]
        public List<Guid>? GenreIdList { get; set; } = new List<Guid>();
        #endregion
    }
}
