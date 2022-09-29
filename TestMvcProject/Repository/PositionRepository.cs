using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;

namespace TestMvcProject.Repository
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        /// <summary>
        /// Get SelectList for ViewBag with positions.
        /// </summary>
        public async Task<SelectList> FillViewBagPositionListAsync()
        {
            var positions = await _appDbContext.Positions.ToListAsync();

            if (positions == null)
                positions = new List<Position>();

            return new SelectList(positions, "Id", "Name");
        }
        /// <summary>
        /// Get PositionList with searching by Name.
        /// </summary>
        public async Task<IEnumerable<Position>> FillPositionListAsync(string? searchString)
        {
            IEnumerable<Position> PositionList;

            if (searchString != null)
            {
                PositionList = await _appDbContext.Positions
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchString))
                .ToListAsync();

                return PositionList;
            }

            PositionList = await _appDbContext.Positions
                .AsNoTracking()
                .ToListAsync();

            return PositionList;
        }
        /// <summary>
        /// Get sorted PositionList with sortOrder by asceding or descading for field Name.
        /// </summary>
        public IEnumerable<Position> SortPosition(string? sortOrder, IEnumerable<Position> PositionList)
        {
            switch (sortOrder)
            {
                case "Name_asc":
                    PositionList = PositionList.OrderBy(a => a.Name);
                    break;
                case "Name_desc":
                    PositionList = PositionList.OrderByDescending(a => a.Name);
                    break;

                default:
                    PositionList = PositionList.OrderBy(a => a.Name);
                    break;
            }
            return PositionList;
        }

        public async Task<Position> GetPositionById(Guid id, bool asNoTracking)
        {
            Position? position;
            if (asNoTracking)
            {
                position = await _appDbContext.Positions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == id);

                return position;
            }

            position = await _appDbContext.Positions
                    .FirstOrDefaultAsync(a => a.Id == id);

            return position;
        }

        public async Task<Position> GetPositionByIdWithAuthor(Guid id, bool asNoTracking)
        {
            Position? position;
            if (asNoTracking)
            {
                position = await _appDbContext.Positions
                    .AsNoTracking()
                    .Include(p => p.Authors)
                    .FirstOrDefaultAsync(p => p.Id == id);
                return position;
            }

            position = await _appDbContext.Positions
                .Include(p => p.Authors)
                .FirstOrDefaultAsync(p => p.Id == id);

            return position;
        }
    }
}
