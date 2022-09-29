using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Repository.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        /// <summary>
        /// Get SelectList for ViewBag with genres.
        /// </summary>
        public Task<SelectList> FillViewBagGenreListAsync();
        public DbSet<Genre> GetAll();
        public Task<List<Genre>> GetGenreListById(List<Guid> genreIdList);
    }
}
