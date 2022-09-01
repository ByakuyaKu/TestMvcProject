using Anime = TestMvcProject.Models.Anime;

namespace TestMvcProject.Jikan.Interfaces
{
    public interface IAnimeLib
    {
        public Task<List<Anime>> GetTopAnimeAsync();
        public Task<Anime> GetAnimeAsync(long id);
    }
}
