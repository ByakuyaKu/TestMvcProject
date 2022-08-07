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
        public DateTime MangaStarts { get; set; }
        public DateTime MangaEnds { get; set; }
        public int ChaptersCount { get; set; }
        public int ChaptersRealesed { get; set; }
        public string Description { get; set; }

        public List<Anime> Animies { get; set; }
        public List<Author> Authors { get; set; }
        public List<Image> Images { get; set; }
    }
}
