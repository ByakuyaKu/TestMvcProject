using TestMvcProject.Models;

namespace TestMvcProject.Jikan
{
    public interface IMangaLogicService
    {
        public Task<List<Manga>> GetTopManga();
        public Task<Manga> GetMangaAsync(long id);
    }
}
