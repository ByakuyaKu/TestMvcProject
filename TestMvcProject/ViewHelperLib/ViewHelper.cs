using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Microsoft.AspNetCore.Mvc;
using Anime = TestMvcProject.Models.Anime;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;
using Manga = TestMvcProject.Models.Manga;
using TestMvcProject.Jikan.Libs;

namespace TestMvcProject.ViewHelperLib
{
    public class ViewHelper : IViewHelper
    {
        /// <summary>
        /// IFormFile to Image.
        /// </summary>
        public Image GetImg(IFormFile avatar, string fileName)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)avatar.Length);
            }
            var img = new Image();
            img.Data = imageData;
            img.Name = fileName;
            return img;
        }
        /// <summary>
        /// Get SelectList for ViewBag with authors include positions and images.
        /// </summary>
        public async Task<SelectList> FillViewBagAuthorListAsync(AppDbContext _appDbContext)
        {
            var authors = await _appDbContext.Authors
                .Include(p => p.Positions)
                .Include(a => a.Images).ToListAsync();

            if (authors == null)
                authors = new List<Author>();

            return new SelectList(authors, "Id", "FirstName", "LastName");
        }
        /// <summary>
        /// Get SelectList for ViewBag with animies include manga and images.
        /// </summary>
        public async Task<SelectList> FillViewBagAnimeListAsync(AppDbContext _appDbContext)
        {
            var animies = await _appDbContext.Anime
                .Include(a => a.Images)
                .Include(a => a.Manga).ToListAsync();

            if (animies == null)
                animies = new List<Anime>();

            return new SelectList(animies, "Id", "Tittle");
        }
        /// <summary>
        /// Get SelectList for ViewBag with mangas include anime and images.
        /// </summary>
        public async Task<SelectList> FillViewBagMangaListAsync(AppDbContext _appDbContext)
        {
            var mangas = await _appDbContext.Manga
                .Include(a => a.Anime)
                .Include(m => m.Images).ToListAsync();

            if (mangas == null)
                mangas = new List<Manga>();

            return new SelectList(mangas, "Id", "Tittle");
        }
        /// <summary>
        /// Get SelectList for ViewBag with genres.
        /// </summary>
        public async Task<SelectList> FillViewBagGenreListAsync(AppDbContext _appDbContext)
        {
            var genres = await _appDbContext.Genres.ToListAsync();

            if (genres == null)
                genres = new List<Genre>();

            return new SelectList(genres, "Id", "Name");
        }
        /// <summary>
        /// Get SelectList for ViewBag with positions.
        /// </summary>
        public async Task<SelectList> FillViewBagPositionListAsync(AppDbContext _appDbContext)
        {
            var positions = await _appDbContext.Positions.ToListAsync();

            if (positions == null)
                positions = new List<Position>();

            return new SelectList(positions, "Id", "Name");
        }
        /// <summary>
        /// Get images for authors.
        /// </summary>
        public async Task<List<Author>?> SearchAuthorsImagesAsync(List<Author>? authors, AppDbContext _appDbContext)
        {
            if (authors != null)
                for (int i = 0; i < authors.Count; i++)
                {
                    authors[i].Images = await _appDbContext.Images.Where(im => im.AuthorId == authors[i].Id).ToListAsync();
                }

            return authors;
        }
        /// <summary>
        /// Get images for mangas.
        /// </summary>
        public async Task<List<Manga>?> SearchMangasImagesAsync(List<Manga>? mangas, AppDbContext _appDbContext)
        {
            if (mangas != null)
                for (int i = 0; i < mangas.Count; i++)
                {
                    mangas[i].Images = await _appDbContext.Images.Where(m => m.MangaId == mangas[i].Id).ToListAsync();
                }

            return mangas;
        }
        /// <summary>
        /// Get images for animies.
        /// </summary>
        public async Task<List<Anime>?> SearchAnimiesImagesAsync(List<Anime>? animies, AppDbContext _appDbContext)
        {
            if (animies != null)
                for (int i = 0; i < animies.Count; i++)
                {
                    animies[i].Images = await _appDbContext.Images.Where(a => a.AnimeId == animies[i].Id).ToListAsync();
                }

            return animies;
        }
        /// <summary>
        /// Get AnimeList with searching by Tittle.
        /// </summary>
        public async Task<IEnumerable<Anime>> FillAnimeListAsync(string? searchString, AppDbContext _appDbContext)
        {
            IEnumerable<Anime> AnimeList;
            if (searchString != null)
            {
                AnimeList = await _appDbContext.Anime
                     .AsNoTracking()
                     .Include(a => a.Images)
                     .Where(a => a.Tittle.Contains(searchString))
                     .ToListAsync();

                return AnimeList;
            }

            AnimeList = await _appDbContext.Anime
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();

            return AnimeList;
        }
        /// <summary>
        /// Get MangaList with searching by Tittle.
        /// </summary>
        public async Task<IEnumerable<Manga>> FillMangaListAsync(string? searchString, AppDbContext _appDbContext)
        {
            IEnumerable<Manga> MangaList;
            if (searchString != null)
            {
                MangaList = await _appDbContext.Manga
                     .AsNoTracking()
                     .Include(m => m.Images)
                     .Where(m => m.Tittle.Contains(searchString))
                     .ToListAsync();

                return MangaList;
            }

            MangaList = await _appDbContext.Manga
                .AsNoTracking()
                .Include(m => m.Images)
                .ToListAsync();

            return MangaList;
        }
        /// <summary>
        /// Get AuthorList with searching by FirstName.
        /// </summary>
        public async Task<IEnumerable<Author>> FillAuthorListAsync(string? searchString, AppDbContext _appDbContext)
        {
            IEnumerable<Author> AuthorList;
            if (searchString != null)
            {
                AuthorList = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Images)
                .Where(a => a.FirstName.Contains(searchString))
                .ToListAsync();

                return AuthorList;
            }

            AuthorList = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();

            return AuthorList;
        }
        /// <summary>
        /// Get PositionList with searching by Name.
        /// </summary>
        public async Task<IEnumerable<Position>> FillPositionListAsync(string? searchString, AppDbContext _appDbContext)
        {
            IEnumerable<Position> PositionList;

            if (searchString != null)
            {
                PositionList = await _appDbContext.Positions
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchString))
                .ToListAsync();

                return PositionList;
            }

            PositionList = await _appDbContext.Positions
                .AsNoTracking()
                .ToListAsync();

            return PositionList;
        }
        /// <summary>
        /// Get sorted AuthorList with sortOrder by asceding or descading for fields: MemberFavorites or FirstName.
        /// </summary>
        public IEnumerable<Author> SortAuthor(string? sortOrder, IEnumerable<Author> AuthorList)
        {
            switch (sortOrder)
            {

                case "Popularity_asc":
                    AuthorList = AuthorList.OrderBy(a => a.MemberFavorites);
                    break;
                case "Popularity_desc":
                    AuthorList = AuthorList.OrderByDescending(a => a.MemberFavorites);
                    break;

                case "Name_asc":
                    AuthorList = AuthorList.OrderBy(a => a.FirstName);
                    break;
                case "Name_desc":
                    AuthorList = AuthorList.OrderByDescending(a => a.FirstName);
                    break;

                default:
                    AuthorList = AuthorList.OrderBy(a => a.FirstName);
                    break;
            }
            return AuthorList;
        }
        /// <summary>
        /// Get sorted PositionList with sortOrder by asceding or descading for field Name.
        /// </summary>
        public IEnumerable<Position> SortPosition(string? sortOrder, IEnumerable<Position> PositionList)
        {
            switch (sortOrder)
            {
                case "Name_asc":
                    PositionList = PositionList.OrderBy(a => a.Name);
                    break;
                case "Name_desc":
                    PositionList = PositionList.OrderByDescending(a => a.Name);
                    break;

                default:
                    PositionList = PositionList.OrderBy(a => a.Name);
                    break;
            }
            return PositionList;
        }
        /// <summary>
        /// Get sorted AnimeList with sortOrder by asceding or descading for fields: Score or Popularity or Tittle or Rank.
        /// </summary>
        public IEnumerable<Anime> SortAnime(string? sortOrder, IEnumerable<Anime> AnimeList)
        {
            switch (sortOrder)
            {
                case "Score_desc":
                    AnimeList = AnimeList.OrderByDescending(a => a.Score);
                    //ViewData["CurrentSortView"] = "score desc";
                    break;
                case "Score_asc":
                    AnimeList = AnimeList.OrderBy(a => a.Score);
                    //ViewData["CurrentSortView"] = "score asc";
                    break;

                case "Popularity_asc":
                    AnimeList = AnimeList.OrderBy(a => a.Popularity);
                    //ViewData["CurrentSortView"] = "popularity asc";
                    break;
                case "Popularity_desc":
                    AnimeList = AnimeList.OrderByDescending(a => a.Popularity);
                    //ViewData["CurrentSortView"] = "popularity desc";
                    break;

                case "Name_asc":
                    AnimeList = AnimeList.OrderBy(a => a.Tittle);
                    //ViewData["CurrentSortView"] = "name asc";
                    break;
                case "Name_desc":
                    AnimeList = AnimeList.OrderByDescending(a => a.Tittle);
                    //ViewData["CurrentSortView"] = "name desc";
                    break;

                case "Rank_asc":
                    AnimeList = AnimeList.OrderBy(a => a.Rank);
                    //ViewData["CurrentSortView"] = "rank asc";
                    break;
                case "Rank_desc":
                    AnimeList = AnimeList.OrderByDescending(a => a.Rank);
                    //ViewData["CurrentSortView"] = "rank desc";
                    break;

                default:
                    AnimeList = AnimeList.OrderBy(a => a.Rank);
                    //ViewData["CurrentSortView"] = "rank asc";
                    break;
            }
            return AnimeList;
        }
        /// <summary>
        /// Get sorted MangaList with sortOrder by asceding or descading for fields: Score or Popularity or Tittle or Rank.
        /// </summary>
        public IEnumerable<Manga> SortManga(string? sortOrder, IEnumerable<Manga> MangaList)
        {
            switch (sortOrder)
            {
                case "Score_desc":
                    MangaList = MangaList.OrderByDescending(a => a.Score);
                    break;

                case "Score_asc":
                    MangaList = MangaList.OrderBy(a => a.Score);
                    break;

                case "Popularity_asc":
                    MangaList = MangaList.OrderBy(a => a.Popularity);
                    break;
                case "Popularity_desc":
                    MangaList = MangaList.OrderByDescending(a => a.Popularity);
                    break;

                case "Name_asc":
                    MangaList = MangaList.OrderBy(a => a.Tittle);
                    break;
                case "Name_desc":
                    MangaList = MangaList.OrderByDescending(a => a.Tittle);
                    break;

                case "Rank_asc":
                    MangaList = MangaList.OrderBy(a => a.Rank);
                    break;
                case "Rank_desc":
                    MangaList = MangaList.OrderByDescending(a => a.Rank);
                    break;

                default:
                    MangaList = MangaList.OrderBy(a => a.Rank);
                    break;
            }
            return MangaList;
        }
    }
}
