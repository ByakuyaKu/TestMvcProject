using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ItemCreation { get; set; } = DateTime.Now;
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
