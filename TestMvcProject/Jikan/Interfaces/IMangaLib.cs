using TestMvcProject.Models;

namespace TestMvcProject.Jikan.Interfaces
{
    public interface IMangaLib
    {
        public Task<List<Manga>> GetTopMangaAsync();
        public Task<Manga> GetMangaAsync(long id);
    }
}
