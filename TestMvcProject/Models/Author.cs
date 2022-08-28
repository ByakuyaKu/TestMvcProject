using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AdditionalName { get; set; }
        public int? MemberFavorites { get; set; }
        public string? WebsiteUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string? About { get; set; }
        //public bool IsDeleted { get; set; } = false;
        public DateTime ItemCreation { get; set; } = DateTime.Now;

        [NotMapped]
        public IFormFile? Avatar { get; set; }

        //foreign keys
        #region
        public Guid? MangaId { get; set; }
        [ForeignKey("MangaId")]
        public List<Manga>? Manga { get; set; } = new List<Manga>();

        public Guid? AnimeId { get; set; }
        [ForeignKey("AnimeId")]
        public List<Anime>? Anime { get; set; } = new List<Anime>();

        //public Guid? PositionId { get; set; }
        //[ForeignKey("PositionId")]
        public List<Position>? Positions { get; set; } = new List<Position>();
        public List<Image>? Images { get; set; } = new List<Image>();
        #endregion

    }
}
