using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Repository.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        /// <summary>
        /// Get SelectList for ViewBag with authors include positions and images.
        /// </summary>
        public Task<SelectList> FillViewBagAuthorListAsync();
        /// <summary>
        /// Get images for authors.
        /// </summary>
        public Task<List<Author>?> SearchAuthorsImagesAsync(List<Author>? authors);
        /// <summary>
        /// Get AuthorList with searching by FirstName.
        /// </summary>
        public Task<IEnumerable<Author>> FillAuthorListAsync(string? searchString);
        /// <summary>
        /// Get sorted AuthorList with sortOrder by asceding or descading for fields: MemberFavorites or FirstName.
        /// </summary>
        public IEnumerable<Author> SortAuthor(string? sortOrder, IEnumerable<Author> AuthorList);

        public DbSet<Author> GetAll();

        public Task<List<Author>> GetAuthorListByIdAsync(List<Guid> authorIdList);

        public Task<Author> GetAuthorByIdFullInfoAsync(Guid id, bool asNoTracking);
        public Task<Author> GetAuthorByIdWithImagesAsync(Guid id, bool asNoTracking);
        public Task<Author> GetAuthorByIdWithAnimeMangaPositionAsync(Guid id, bool asNoTracking);

        public Task LoadRealatedMangaAsync(Author author);
        public Task LoadRealatedPositionsAsync(Author author);
        public Task LoadRealatedAnimiesAsync(Author author);
    }
}
