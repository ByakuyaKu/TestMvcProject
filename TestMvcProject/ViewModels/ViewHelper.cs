using JikanDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Anime = TestMvcProject.Models.Anime;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.ViewModels
{
    public class ViewHelper : IViewHelper
    {
        public Image GetImg(IFormFile avatar, string fileName)
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

        public async Task<SelectList> FillViewBagAuthorListAsync(AppDbContext _appDbContext)
        {
            var authors = await _appDbContext.Authors
                .Include(p => p.Positions)
                .Include(a => a.Images).ToListAsync();

            if (authors == null)
                authors = new List<Author>();

            return new SelectList(authors, "Id", "FirstName", "LastName");
        }

        public async Task<SelectList> FillViewBagAnimeListAsync(AppDbContext _appDbContext)
        {
            var animies = await _appDbContext.Animies
                .Include(a => a.Images)
                .Include(a => a.Manga).ToListAsync();

            if (animies == null)
                animies = new List<Anime>();

            return new SelectList(animies, "Id", "Tittle");
        }

        public async Task<SelectList> FillViewBagMangaListAsync(AppDbContext _appDbContext)
        {
            var mangas = await _appDbContext.Mangas
                .Include(a => a.Animies)
                .Include(m => m.Images).ToListAsync();

            if (mangas == null)
                mangas = new List<Manga>();

            return new SelectList(mangas, "Id", "Tittle");
        }

        public async Task<SelectList> FillViewBagGenreListAsync(AppDbContext _appDbContext)
        {
            var genres = await _appDbContext.Genres.ToListAsync();

            if (genres == null)
                genres = new List<Genre>();

            return new SelectList(genres, "Id", "Name");
        }

        public async Task<List<Author>?> SearchAuthorsImagesAsync(List<Author>? authors, AppDbContext _appDbContext)
        {
            if (authors != null)
                for (int i = 0; i < authors.Count; i++)
                {
                    authors[i].Images = await _appDbContext.Images.Where(im => im.AuthorId == authors[i].Id).ToListAsync();
                }

            return authors;
        }
    }
}
