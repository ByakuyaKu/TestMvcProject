using Microsoft.AspNetCore.Mvc.Rendering;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.ViewHelperLib
{
    /// <summary>
    /// IViewHelper is interface that contain declarations of methods for Views logic.
    /// </summary>
    public interface IViewHelper
    {
        /// <summary>
        /// IFormFile to Image.
        /// </summary>
        public Image GetImg(IFormFile avatar, string fileName);
        /// <summary>
        /// Get SelectList for ViewBag with authors include positions and images.
        /// </summary>
        public Task<SelectList> FillViewBagAuthorListAsync(AppDbContext _appDbContext);
        /// <summary>
        /// Get SelectList for ViewBag with animies include manga and images.
        /// </summary>
        public Task<SelectList> FillViewBagAnimeListAsync(AppDbContext _appDbContext);
        /// <summary>
        /// Get SelectList for ViewBag with mangas include anime and images.
        /// </summary>
        public Task<SelectList> FillViewBagMangaListAsync(AppDbContext _appDbContext);
        /// <summary>
        /// Get SelectList for ViewBag with genres.
        /// </summary>
        public Task<SelectList> FillViewBagGenreListAsync(AppDbContext _appDbContext);
        /// <summary>
        /// Get SelectList for ViewBag with positions.
        /// </summary>
        public Task<SelectList> FillViewBagPositionListAsync(AppDbContext _appDbContext);
        /// <summary>
        /// Get images for authors.
        /// </summary>
        public Task<List<Author>?> SearchAuthorsImagesAsync(List<Author>? authors, AppDbContext _appDbContext);
        /// <summary>
        /// Get images for mangas.
        /// </summary>
        public Task<List<Manga>?> SearchMangasImagesAsync(List<Manga>? mangas, AppDbContext _appDbContext);
        /// <summary>
        /// Get images for animies.
        /// </summary>
        public Task<List<Anime>?> SearchAnimiesImagesAsync(List<Anime>? animies, AppDbContext _appDbContext);
        /// <summary>
        /// Get MangaList with searching by Tittle.
        /// </summary>
        public Task<IEnumerable<Manga>> FillMangaListAsync(string? searchString, AppDbContext _appDbContext);
        /// <summary>
        /// Get AnimeList with searching by Tittle.
        /// </summary>
        public Task<IEnumerable<Anime>> FillAnimeListAsync(string? searchString, AppDbContext _appDbContext);
        /// <summary>
        /// Get AuthorList with searching by FirstName.
        /// </summary>
        public Task<IEnumerable<Author>> FillAuthorListAsync(string? searchString, AppDbContext _appDbContext);
        /// <summary>
        /// Get PositionList with searching by Name.
        /// </summary>
        public Task<IEnumerable<Position>> FillPositionListAsync(string? searchString, AppDbContext _appDbContext);
        /// <summary>
        /// Get sorted MangaList with sortOrder by asceding or descading for fields: Score or Popularity or Tittle or Rank.
        /// </summary>
        public IEnumerable<Manga> SortManga(string? sortOrder, IEnumerable<Manga> MangaList);
        /// <summary>
        /// Get sorted AuthorList with sortOrder by asceding or descading for fields: MemberFavorites or FirstName.
        /// </summary>
        public IEnumerable<Author> SortAuthor(string? sortOrder, IEnumerable<Author> AuthorList);
        /// <summary>
        /// Get sorted AnimeList with sortOrder by asceding or descading for fields: Score or Popularity or Tittle or Rank.
        /// </summary>
        public IEnumerable<Anime> SortAnime(string? sortOrder, IEnumerable<Anime> AnimeList);
        /// <summary>
        /// Get sorted PositionList with sortOrder by asceding or descading for field Name.
        /// </summary>
        public IEnumerable<Position> SortPosition(string? sortOrder, IEnumerable<Position> PositionList);

    }
}
