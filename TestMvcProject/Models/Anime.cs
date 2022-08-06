using System.ComponentModel.DataAnnotations;

namespace TestMvcProject.Models
{
    public class Anime
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Tittle { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;
        public DateTime AnimeStarts { get; set; }
        public DateTime AnimeEnds { get; set; }
        public int SeriesCount { get; set; }
        public int SeriesRealesed { get; set; }
        public string ProducerFirstName { get; set; }
        public string ProducerSecondName { get; set; }
        public string ProducerThirdName { get; set; }
        public Guid MangaId { get; set; }

    }
}
