using System.ComponentModel.DataAnnotations;

namespace TestMvcProject.Models
{
    public class Manga
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Tittle { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;
        public DateTime MangaStarts { get; set; }
        public DateTime MangaEnds { get; set; }
        public int ChaptersCount { get; set; }
        public int ChaptersRealesed { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorSecondName { get; set; }
        public string AuthorThirdName { get; set; }
        public Guid AnimeId { get; set; }
    }
}
