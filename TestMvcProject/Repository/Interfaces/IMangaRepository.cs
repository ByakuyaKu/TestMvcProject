using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Repository.Interfaces
{
    public interface IMangaRepository : IRepository<Manga>
    {
        /// <summary>
        /// Get SelectList for ViewBag with mangas include anime and images.
        /// </summary>
        public Task<SelectList> FillViewBagMangaListAsync();
        /// <summary>
        /// Get images for mangas.
        /// </summary>
        public Task<List<Manga>?> SearchMangasImagesAsync(List<Manga>? mangas);
        /// <summary>
        /// Get MangaList with searching by Tittle.
        /// </summary>
        public Task<IEnumerable<Manga>> FillMangaListAsync(string? searchString);
        /// <summary>
        /// Get sorted MangaList with sortOrder by asceding or descading for fields: Score or Popularity or Tittle or Rank.
        /// </summary>
        public IEnumerable<Manga> SortManga(string? sortOrder, IEnumerable<Manga> MangaList);
        public DbSet<Manga> GetAll();
        public Task<List<Manga>> GetMangaListByIdAsync(List<Guid> mangaIdList);
        public Task<Manga> GetMangaByIdFullInfoAsync(Guid id, bool asNoTracking);
        public Task<Manga> GetMangaByIdWithAnimeAuthorGenreAsync(Guid id, bool asNoTracking);
        public Task<Manga> GetMangaByIdWithImageAsync(Guid id, bool asNoTracking);

        public Task LoadRealatedAnimiesAsync(Manga manga);
        public Task LoadRealatedGenresAsync(Manga manga);
        public Task LoadRealatedAuthorsAsync(Manga manga);
    }
}
