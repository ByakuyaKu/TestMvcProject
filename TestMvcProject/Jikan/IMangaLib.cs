using TestMvcProject.Models;

namespace TestMvcProject.Jikan
{
    public interface IMangaLib
    {
        public Task<List<Manga>> GetTopMangaAsync();
        public Task<Manga> GetMangaAsync(long id);
    }
}
