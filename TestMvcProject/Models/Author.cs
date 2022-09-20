﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMvcProject.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(AllowEmptyStrings = false)]
        public long MalId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AdditionalName { get; set; }
        public int? MemberFavorites { get; set; }
        public string? WebsiteUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string? About { get; set; }
        //public bool IsDeleted { get; set; } = false;
        public DateTime ItemCreation { get; set; } = DateTime.Now;

        //foreign keys
        #region
        [ForeignKey("MangaId")]
        public List<Manga>? Manga { get; set; } = new List<Manga>();

        [ForeignKey("AnimeId")]
        public List<Anime>? Anime { get; set; } = new List<Anime>();

        [ForeignKey("PositionId")]
        public List<Position>? Positions { get; set; } = new List<Position>();
        [ForeignKey("ImageId")]
        public List<Image>? Images { get; set; } = new List<Image>();
        #endregion

        //NotMapped fields
        #region
        [NotMapped]
        public IFormFile? Avatar { get; set; }
        [NotMapped]
        public List<Guid>? AnimeIdList { get; set; } = new List<Guid>();
        [NotMapped]
        public List<Guid>? MangaIdList { get; set; } = new List<Guid>();
        [NotMapped]
        public List<Guid>? PositionIdList { get; set; } = new List<Guid>();
        #endregion

    }
}
