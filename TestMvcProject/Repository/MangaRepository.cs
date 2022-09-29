using JikanDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Repository
{
    public class MangaRepository : Repository<Manga>, IMangaRepository
    {
        public MangaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        /// <summary>
        /// Get SelectList for ViewBag with mangas include anime and images.
        /// </summary>
        public async Task<SelectList> FillViewBagMangaListAsync()
        {
            var mangas = await _appDbContext.Manga
                .Include(a => a.Anime)
                .Include(m => m.Images).ToListAsync();

            if (mangas == null)
                mangas = new List<Manga>();

            return new SelectList(mangas, "Id", "Tittle");
        }
        /// <summary>
        /// Get images for mangas.
        /// </summary>
        public async Task<List<Manga>?> SearchMangasImagesAsync(List<Manga>? mangas)
        {
            if (mangas != null)
                for (int i = 0; i < mangas.Count; i++)
                {
                    mangas[i].Images = await _appDbContext.Images.Where(m => m.MangaId == mangas[i].Id).ToListAsync();
                }

            return mangas;
        }
        /// <summary>
        /// Get MangaList with searching by Tittle.
        /// </summary>
        public async Task<IEnumerable<Manga>> FillMangaListAsync(string? searchString)
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
        public DbSet<Manga> GetAll() => _appDbContext.Manga;

        public async Task<List<Manga>> GetMangaListByIdAsync(List<Guid> mangaIdList)
        {
            var mangaList = await _appDbContext.Manga.Where(m => mangaIdList.Any(ml => ml == m.Id)).ToListAsync();
            return mangaList;
        }

        public async Task<Manga> GetMangaByIdFullInfoAsync(Guid id, bool asNoTracking)
        {
            Manga? manga;
            if (asNoTracking)
            {
                manga = await _appDbContext.Manga
                .AsNoTracking()
                .Include(m => m.Anime)
                //.ThenInclude(a => a.Images)
                .Include(m => m.Images)
                .Include(m => m.Authors)
                //.ThenInclude(a => a.Images)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

                return manga;
            }

            manga = await _appDbContext.Manga
                .Include(m => m.Anime)
                //.ThenInclude(a => a.Images)
                .Include(m => m.Images)
                .Include(m => m.Authors)
                //.ThenInclude(a => a.Images)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            return manga;
        }

        public async Task<Manga> GetMangaByIdWithAnimeAuthorGenreAsync(Guid id, bool asNoTracking)
        {
            Manga? manga;
            if (asNoTracking)
            {
                manga = await _appDbContext.Manga
                .AsNoTracking()
                .Include(m => m.Anime)
                .Include(m => m.Authors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

                return manga;
            }

            manga = await _appDbContext.Manga
                .Include(m => m.Anime)
                .Include(m => m.Authors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            return manga;
        }

        public async Task<Manga> GetMangaByIdWithImageAsync(Guid id, bool asNoTracking)
        {
            Manga? manga;

            if (asNoTracking)
            {
                manga = await _appDbContext.Manga
                .AsNoTracking()
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

                return manga;
            }

            manga = await _appDbContext.Manga
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            return manga;
        }

        public async Task LoadRealatedAnimiesAsync(Manga manga) => await _appDbContext.Entry(manga).Collection(m => m.Anime).LoadAsync();
        public async Task LoadRealatedGenresAsync(Manga manga) => await _appDbContext.Entry(manga).Collection(m => m.Genres).LoadAsync();
        public async Task LoadRealatedAuthorsAsync(Manga manga) => await _appDbContext.Entry(manga).Collection(m => m.Authors).LoadAsync();
    }
}
