using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;
using TestMvcProject.ViewHelperLib;

namespace TestMvcProject.Repository
{
    public class AnimeRepository : Repository<Anime>, IAnimeRepository
    {
        public AnimeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        /// <summary>
        /// Get SelectList for ViewBag with animies include manga and images.
        /// </summary>
        public async Task<SelectList> FillViewBagAnimeListAsync()
        {
            var animies = await _appDbContext.Anime
                .Include(a => a.Images)
                .Include(a => a.Manga).ToListAsync();

            if (animies == null)
                animies = new List<Anime>();

            return new SelectList(animies, "Id", "Tittle");
        }
        /// <summary>
        /// Get images for animies.
        /// </summary>
        public async Task<List<Anime>?> SearchAnimiesImagesAsync(List<Anime>? animies)
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
        public async Task<IEnumerable<Anime>> FillAnimeListAsync(string? searchString)
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

        public async Task<Anime> GetAnimeByIdWithImages(Guid id, bool asNoTracking)
        {
            var anime = await _appDbContext.Anime
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            return anime;
        }

        public async Task<Anime> GetAnimeByIdFullInfo(Guid id, bool asNoTracking)
        {
            Anime? anime;
            if (asNoTracking)
            {
                anime = await _appDbContext.Anime
                    .AsNoTracking()
                    .Include(a => a.Manga)
                    //.ThenInclude(m => m.Images)
                    .Include(a => a.Images)
                    .Include(a => a.Authors)
                    //.ThenInclude(a => a.Images)
                    .Include(a => a.Genres)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return anime;
            }

            anime = await _appDbContext.Anime
                    .Include(a => a.Manga)
                    //.ThenInclude(m => m.Images)
                    .Include(a => a.Images)
                    .Include(a => a.Authors)
                    //.ThenInclude(a => a.Images)
                    .Include(a => a.Genres)
                    .FirstOrDefaultAsync(a => a.Id == id);

            return anime;
        }

        public async Task<Anime> GetAnimeByIdWithMangaAuthorGenres(Guid id, bool asNoTracking)
        {
            Anime? anime;

            if (asNoTracking)
            {
                anime = await _appDbContext.Anime
                .AsNoTracking()
                .Include(a => a.Manga)
                .Include(a => a.Authors)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.Id == id);

                return anime;
            }

            anime = await _appDbContext.Anime
                .Include(a => a.Manga)
                .Include(a => a.Authors)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.Id == id);

            return anime;
        }

        public DbSet<Anime> GetAll() => _appDbContext.Anime;

        public async Task<List<Anime>> GetAnimeListByIdAsync(List<Guid> animeIdList)
        {
            var animeList = await _appDbContext.Anime.Where(a => animeIdList.Any(al => al == a.Id)).ToListAsync();

            return animeList;
        }

        public async Task LoadRealatedMangaAsync(Anime anime) => await _appDbContext.Entry(anime).Collection(a => a.Manga).LoadAsync();
        public async Task LoadRealatedGenresAsync(Anime anime) => await _appDbContext.Entry(anime).Collection(a => a.Genres).LoadAsync();
        public async Task LoadRealatedAuthorsAsync(Anime anime) => await _appDbContext.Entry(anime).Collection(a => a.Authors).LoadAsync();
    }
}
