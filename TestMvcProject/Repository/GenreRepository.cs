using JikanDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;
using Genre = TestMvcProject.Models.Genre;

namespace TestMvcProject.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        /// <summary>
        /// Get SelectList for ViewBag with genres.
        /// </summary>
        public async Task<SelectList> FillViewBagGenreListAsync()
        {
            var genres = await _appDbContext.Genres.ToListAsync();

            if (genres == null)
                genres = new List<Genre>();

            return new SelectList(genres, "Id", "Name");
        }
        public DbSet<Genre> GetAll() => _appDbContext.Genres;
        public async Task<List<Genre>> GetGenreListById(List<Guid> genreIdList)
        {
            var genreList = await _appDbContext.Genres.Where(g => genreIdList.Any(gl => gl == g.Id)).ToListAsync();
            return genreList;
        }
    }
}
