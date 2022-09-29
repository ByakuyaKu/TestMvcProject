using Microsoft.AspNetCore.Mvc.Rendering;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Repository.Interfaces
{
    public interface IPositionRepository : IRepository<Position>
    {
        /// <summary>
        /// Get SelectList for ViewBag with positions.
        /// </summary>
        public Task<SelectList> FillViewBagPositionListAsync();
        /// <summary>
        /// Get PositionList with searching by Name.
        /// </summary>
        public Task<IEnumerable<Position>> FillPositionListAsync(string? searchString);
        /// <summary>
        /// Get sorted PositionList with sortOrder by asceding or descading for field Name.
        /// </summary>
        public IEnumerable<Position> SortPosition(string? sortOrder, IEnumerable<Position> PositionList);
        public Task<Position> GetPositionById(Guid id, bool asNoTracking);
        public Task<Position> GetPositionByIdWithAuthor(Guid id, bool asNoTracking);
    }
}
