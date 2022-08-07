﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Description { get; set; }


        public Guid MangaId { get; set; }
        [ForeignKey("MangaId")]
        public Manga Manga { get; set; }

        public List<Author> Author { get; set; }

        public List<Image> Images { get; set; }

    }
}
