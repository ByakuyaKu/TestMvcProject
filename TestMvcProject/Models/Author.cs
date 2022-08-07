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
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string? About { get; set; }
        //public bool IsDeleted { get; set; } = false;
        public DateTime ItemCreation { get; set; } = DateTime.Now;

        public Guid? MangaId { get; set; }
        [ForeignKey("MangaId")]
        public Manga? Manga { get; set; }

        public Guid? AnimeId { get; set; }
        [ForeignKey("AnimeId")]
        public Anime? Anime { get; set; }

        //public Guid ImageId { get; set; }
        //[ForeignKey("ImageId")]
        //public Image Image { get; set; }

        public List<Position> Positions { get; set; }
        public List<Image>? Images { get; set; }

    }
}
