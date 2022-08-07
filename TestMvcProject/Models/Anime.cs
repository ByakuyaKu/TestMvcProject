using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Anime
    {
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


        public Guid? MangaId { get; set; }
        [ForeignKey("MangaId")]
        public List<Manga>? Manga { get; set; }

        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public List<Author>? Authors { get; set; }

        public List<Image>? Images { get; set; }

    }
}
