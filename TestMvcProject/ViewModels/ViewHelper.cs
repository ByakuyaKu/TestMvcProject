using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.ViewModels
{
    public class ViewHelper
    {
        public static Image GetImg(IFormFile avatar, string fileName)
        {
            byte[] imageData;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)avatar.Length);
            }
            // установка массива байтов
            var img = new Image();
            img.Data = imageData;
            //img.Name = "AvatarOf" + manga.Tittle;
            img.Name = fileName;
            return img;
        }

        public static async Task<SelectList> FillViewBagAuthorList(AppDbContext _appDbContext)
        {
            var authors = await _appDbContext.Authors
                .Include(p => p.Positions)
                .Include(a => a.Images).ToListAsync();

            if (authors == null)
                authors = new List<Author>();

            return new SelectList(authors, "Id", "FirstName", "LastName");
        }

        public static async Task<SelectList> FillViewBagAnimeList(AppDbContext _appDbContext)
        {
            var animies = await _appDbContext.Animies
                .Include(a => a.Images)
                .Include(a => a.Manga).ToListAsync();

            if (animies == null)
                animies = new List<Anime>();

            return new SelectList(animies, "Id", "Tittle");
        }

        public static async Task<SelectList> FillViewBagMangaList(AppDbContext _appDbContext)
        {
            var mangas = await _appDbContext.Mangas
                .Include(a => a.Animies)
                .Include(m => m.Images).ToListAsync();

            if (mangas == null)
                mangas = new List<Manga>();

            return new SelectList(mangas, "Id", "Tittle");
        }

        public static async Task<SelectList> s(AppDbContext _appDbContext)
        {
            var mangas = await _appDbContext.Mangas
                .Include(a => a.Animies)
                .Include(m => m.Images).ToListAsync();

            if (mangas == null)
                mangas = new List<Manga>();

            return new SelectList(mangas, "Id", "Tittle");
        }
    }
}
