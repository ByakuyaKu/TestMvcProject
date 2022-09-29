using JikanDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;

namespace TestMvcProject.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        /// <summary>
        /// Get SelectList for ViewBag with authors include positions and images.
        /// </summary>
        public async Task<SelectList> FillViewBagAuthorListAsync()
        {
            var authors = await _appDbContext.Authors
                .Include(p => p.Positions)
                .Include(a => a.Images).ToListAsync();

            if (authors == null)
                authors = new List<Author>();

            return new SelectList(authors, "Id", "FirstName", "LastName");
        }

        /// <summary>
        /// Get images for authors.
        /// </summary>
        public async Task<List<Author>?> SearchAuthorsImagesAsync(List<Author>? authors)
        {
            if (authors != null)
                for (int i = 0; i < authors.Count; i++)
                {
                    authors[i].Images = await _appDbContext.Images.Where(im => im.AuthorId == authors[i].Id).ToListAsync();
                }

            return authors;
        }

        /// <summary>
        /// Get AuthorList with searching by FirstName.
        /// </summary>
        public async Task<IEnumerable<Author>> FillAuthorListAsync(string? searchString)
        {
            IEnumerable<Author> AuthorList;
            if (searchString != null)
            {
                AuthorList = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Images)
                .Where(a => a.FirstName.Contains(searchString))
                .ToListAsync();

                return AuthorList;
            }

            AuthorList = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();

            return AuthorList;
        }

        /// <summary>
        /// Get sorted AuthorList with sortOrder by asceding or descading for fields: MemberFavorites or FirstName.
        /// </summary>
        public IEnumerable<Author> SortAuthor(string? sortOrder, IEnumerable<Author> AuthorList)
        {
            switch (sortOrder)
            {

                case "Popularity_asc":
                    AuthorList = AuthorList.OrderBy(a => a.MemberFavorites);
                    break;
                case "Popularity_desc":
                    AuthorList = AuthorList.OrderByDescending(a => a.MemberFavorites);
                    break;

                case "Name_asc":
                    AuthorList = AuthorList.OrderBy(a => a.FirstName);
                    break;
                case "Name_desc":
                    AuthorList = AuthorList.OrderByDescending(a => a.FirstName);
                    break;

                default:
                    AuthorList = AuthorList.OrderBy(a => a.FirstName);
                    break;
            }
            return AuthorList;
        }

        public DbSet<Author> GetAll() => _appDbContext.Authors;

        public async Task<List<Author>> GetAuthorListByIdAsync(List<Guid> authorIdList)
        {
            var authorList = await _appDbContext.Authors.Where(a => authorIdList.Any(al => al == a.Id)).ToListAsync();

            return authorList;
        }

        public async Task<Author> GetAuthorByIdFullInfoAsync(Guid id, bool asNoTracking)
        {
            Author? author;
            if (asNoTracking)
            {
                author = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Manga)
                //.ThenInclude(m => m.Images)
                .Include(a => a.Images)
                .Include(a => a.Anime)
                .Include(a => a.Positions)
                //.ThenInclude(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == id);

                return author;
            }

            author = await _appDbContext.Authors
                .Include(a => a.Manga)
                //.ThenInclude(m => m.Images)
                .Include(a => a.Images)
                .Include(a => a.Anime)
                .Include(a => a.Positions)
                //.ThenInclude(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public async Task<Author> GetAuthorByIdWithImagesAsync(Guid id, bool asNoTracking)
        {
            Author? author;

            if (asNoTracking)
            {
                author = await _appDbContext.Authors
                .AsNoTracking()
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

                return author;
            }

            author = await _appDbContext.Authors
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            return author;
        }

        public async Task<Author> GetAuthorByIdWithAnimeMangaPositionAsync(Guid id, bool asNoTracking)
        {
            Author? author;
            if (asNoTracking)
            {
                author = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Anime)
                .Include(a => a.Manga)
                .Include(a => a.Positions)
                .FirstOrDefaultAsync(a => a.Id == id);

                return author;
            }

            author = await _appDbContext.Authors
                .Include(a => a.Anime)
                .Include(a => a.Manga)
                .Include(a => a.Positions)
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public async Task LoadRealatedMangaAsync(Author author) => await _appDbContext.Entry(author).Collection(a => a.Manga).LoadAsync();
        public async Task LoadRealatedPositionsAsync(Author author) => await _appDbContext.Entry(author).Collection(a => a.Positions).LoadAsync();
        public async Task LoadRealatedAnimiesAsync(Author author) => await _appDbContext.Entry(author).Collection(a => a.Anime).LoadAsync();
    }
}
