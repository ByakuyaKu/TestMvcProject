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

        //foreign keys
        #region
        public List<Author>? Authors { get; set; } = new List<Author>();

        //public List<Anime>? Anime { get; set; } = new List<Anime>();
        #endregion
    }
}
