using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Models;

namespace TestMvcProject.Repository.Interfaces
{
    public interface IAnimeRepository : IRepository<Anime>
    {
        /// <summary>
        /// Get SelectList for ViewBag with animies include manga and images.
        /// </summary>
        public Task<SelectList> FillViewBagAnimeListAsync();
        /// <summary>
        /// Get images for animies.
        /// </summary>
        public Task<List<Anime>?> SearchAnimiesImagesAsync(List<Anime>? animies);
        /// <summary>
        /// Get AnimeList with searching by Tittle.
        /// </summary>
        public Task<IEnumerable<Anime>> FillAnimeListAsync(string? searchString);
        /// <summary>
        /// Get sorted AnimeList with sortOrder by asceding or descading for fields: Score or Popularity or Tittle or Rank.
        /// </summary>
        public IEnumerable<Anime> SortAnime(string? sortOrder, IEnumerable<Anime> AnimeList);

        public Task<Anime> GetAnimeByIdWithImages(Guid id, bool asNoTracking);
        public Task<Anime> GetAnimeByIdFullInfo(Guid id, bool asNoTracking);
        public Task<Anime> GetAnimeByIdWithMangaAuthorGenres(Guid id, bool asNoTracking);
        public Task<List<Anime>> GetAnimeListByIdAsync(List<Guid> animeIdList);
        public DbSet<Anime> GetAll();
        public Task LoadRealatedMangaAsync(Anime anime);
        public Task LoadRealatedGenresAsync(Anime anime);
        public Task LoadRealatedAuthorsAsync(Anime anime);
    }
}
