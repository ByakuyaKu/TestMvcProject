using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public byte[] Data { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;

        public Guid AnimeId { get; set; }
        [ForeignKey("AnimeId")]
        public Anime Anime { get; set; }

        public Guid MangaId { get; set; }
        [ForeignKey("MangaId")]
        public Manga Manga { get; set; }

        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

    }
}
