using TestMvcProject.Models;

namespace TestMvcProject.Jikan.Interfaces
{
    /// <summary>
    /// IMangaLib is interface that provide Jikan Api adoptation methods for manga.
    /// </summary>
    public interface IMangaLib
    {
        /// <summary>
        /// Get 25 best mangas from MAL by Jikan Api and save this data into db.
        /// </summary>
        public Task<List<Manga>> GetTopMangaAsync();
        /// <summary>
        /// Get manga by id from MAL by Jikan Api and save this data into db.
        /// </summary>
        public Task<Manga> GetMangaAsync(long id);
    }
}
