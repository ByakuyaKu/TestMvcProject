using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Manga
    {
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
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        public Guid? AnimeId { get; set; }
        [ForeignKey("AnimeId")]
        public List<Anime>? Animies { get; set; } = new List<Anime>();

        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public List<Author>? Authors { get; set; } = new List<Author>();
        public List<Image>? Images { get; set; } = new List<Image>();
    }
}
