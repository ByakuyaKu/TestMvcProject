using Microsoft.AspNetCore.Mvc.Rendering;
using TestMvcProject.Models;

namespace TestMvcProject.ViewModels
{
    public class MangaViewModel : GeneralViewModel
    {
        public Manga? Manga { get; set; }
        public SelectList? AnimeList { get; set; }
        public SelectList? AuthorList { get; set; }
    }
}
