using Microsoft.AspNetCore.Mvc.Rendering;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.ViewModels
{
    public interface IViewHelper
    {
        public Image GetImg(IFormFile avatar, string fileName);

        public  Task<SelectList> FillViewBagAuthorListAsync(AppDbContext _appDbContext);

        public  Task<SelectList> FillViewBagAnimeListAsync(AppDbContext _appDbContext);

        public  Task<SelectList> FillViewBagMangaListAsync(AppDbContext _appDbContext);

        public  Task<SelectList> FillViewBagGenreListAsync(AppDbContext _appDbContext);

        public Task<List<Author>?> SearchAuthorsImagesAsync(List<Author>? authors, AppDbContext _appDbContext);




    }
}
