using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required(AllowEmptyStrings = false)]
        //[Index(IsUnique = true)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;

        //foreign keys
        #region
        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public List<Author>? Authors { get; set; } = new List<Author>();

        public Guid? AnimeId { get; set; }
        [ForeignKey("AuthorId")]
        public List<Anime>? Animies { get; set; } = new List<Anime>();
        #endregion
    }
}
