using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;

        //foreign keys
        #region
        public Guid? MangaId { get; set; }
        [ForeignKey("MangaId")]
        public List<Manga>? Manga { get; set; } = new List<Manga>();

        public Guid? AnimeId { get; set; }
        [ForeignKey("AnimeId")]
        public List<Anime>? Anime { get; set; } = new List<Anime>();
        #endregion
    }
}
