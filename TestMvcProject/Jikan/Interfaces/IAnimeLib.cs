using Anime = TestMvcProject.Models.Anime;

namespace TestMvcProject.Jikan.Interfaces
{
    /// <summary>
    /// IAnimeLib is interface that provide Jikan Api adoptation methods for anime.
    /// </summary>
    public interface IAnimeLib
    {
        /// <summary>
        /// Get 25 best animies from MAL by Jikan Api and save this data into db.
        /// </summary>
        public Task<List<Anime>> GetTopAnimeAsync();
        /// <summary>
        /// Get anime by id from MAL by Jikan Api and save this data into db.
        /// </summary>
        public Task<Anime> GetAnimeAsync(long id);
    }
}
