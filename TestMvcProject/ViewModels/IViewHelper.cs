using Microsoft.AspNetCore.Mvc.Rendering;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.ViewModels
{
    public interface IViewHelper
    {
        public Image GetImg(IFormFile avatar, string fileName);

        public  Task<SelectList> FillViewBagAuthorList(AppDbContext _appDbContext);

        public  Task<SelectList> FillViewBagAnimeList(AppDbContext _appDbContext);

        public  Task<SelectList> FillViewBagMangaList(AppDbContext _appDbContext);

        public  Task<SelectList> FillViewBagGenreList(AppDbContext _appDbContext);



    }
}
