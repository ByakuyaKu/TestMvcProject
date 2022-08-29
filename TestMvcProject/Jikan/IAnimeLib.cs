using JikanDotNet;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Anime = TestMvcProject.Models.Anime;

namespace TestMvcProject.Jikan
{
    public interface IAnimeLib  
    {
        public Task<List<Anime>> GetTopAnimeAsync();
        public Task<Anime> GetAnimeAsync(long id);
    }
}
