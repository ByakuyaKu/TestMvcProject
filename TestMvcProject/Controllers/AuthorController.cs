using JikanDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewHelperLib;
using Anime = TestMvcProject.Models.Anime;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IViewHelper _viewHelper;


        public AuthorController(AppDbContext appDbContext, IViewHelper viewHelper)
        {
            _appDbContext = appDbContext;
            _viewHelper = viewHelper;
        }

        // GET: Author
        public async Task<IActionResult> Index(string? sortOrder, string? searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentSortView"] = sortOrder?.Replace("_", " ").ToLower();


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Author> AuthorList = await _viewHelper.FillAuthorListAsync(searchString, _appDbContext);

            AuthorList = _viewHelper.SortAuthor(sortOrder, AuthorList);

            int pageSize = 10;
            return View(await PaginatedList<Author>.CreateAsync(AuthorList, pageNumber ?? 1, pageSize));
        }

        // GET: Author
        public async Task<IActionResult> IndexUneditable(string? sortOrder, string? searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentSortView"] = sortOrder?.Replace("_", " ").ToLower();


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Author> AuthorList = await _viewHelper.FillAuthorListAsync(searchString, _appDbContext);

            AuthorList = _viewHelper.SortAuthor(sortOrder, AuthorList);

            int pageSize = 10;
            return View(await PaginatedList<Author>.CreateAsync(AuthorList, pageNumber ?? 1, pageSize));
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var author = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Manga)
                //.ThenInclude(m => m.Images)
                .Include(a => a.Images)
                .Include(a => a.Anime)
                .Include(a => a.Positions)
                //.ThenInclude(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return NotFound();

            await _viewHelper.SearchAnimiesImagesAsync(author.Anime, _appDbContext);
            await _viewHelper.SearchMangasImagesAsync(author.Manga, _appDbContext);

            if (author.Images != null && author.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(author.Images.Last().Data)));

            return View(author);
        }

        // GET: Author/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AnimeList = await _viewHelper.FillViewBagAnimeListAsync(_appDbContext);
            ViewBag.MangaList = await _viewHelper.FillViewBagMangaListAsync(_appDbContext);
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);

            var animies = _appDbContext.Anime;
            var mangas = _appDbContext.Manga;

            if (author.Avatar != null)
            {
                var img = _viewHelper.GetImg(author.Avatar, "AvatarOf" + author.FirstName);
                author.Images?.Add(img);

            }

            if (author.AnimeIdList != null && author.AnimeIdList.Count > 0)
                author.Anime?.AddRange(animies.Where(a => author.AnimeIdList.Any(m => m == a.Id)).ToList());

            if (author.MangaIdList != null && author.MangaIdList.Count > 0)
                author.Manga?.AddRange(mangas.Where(a => author.MangaIdList.Any(m => m == a.Id)).ToList());

            _appDbContext.Authors.Add(author);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Author created successfully!";

            return RedirectToAction("Index");
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || Guid.Empty == id)
                return NotFound();

            var author = await _appDbContext.Authors
                .Include(a => a.Images)
                .Include(a => a.Manga)
                .Include(a => a.Anime)
                .Include(a => a.Positions)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return NotFound();

            ViewBag.AnimeList = await _viewHelper.FillViewBagAnimeListAsync(_appDbContext);
            ViewBag.MangaList = await _viewHelper.FillViewBagMangaListAsync(_appDbContext);

            return View(author);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (author == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(author);

            var _manga = await _appDbContext.Authors
                .Include(a => a.Anime)
                .Include(a => a.Manga)
                .Include(a => a.Positions)
                .FirstOrDefaultAsync(a => a.Id == author.Id);

            author.Anime = _manga?.Anime;
            author.Manga = _manga?.Manga;
            author.Positions = _manga?.Positions;

            if (author.Avatar != null)
            {
                var img = _viewHelper.GetImg(author.Avatar, "AvatarOf" + author.FirstName);

                _appDbContext.Images.Add(img);
                await _appDbContext.SaveChangesAsync();

                author.Images?.Add(img);
            }

            if (author.AnimeIdList != null && author.AnimeIdList.Count > 0)
            {
                var animeList = await _appDbContext.Anime.Where(a => author.AnimeIdList.Any(m => m == a.Id)).ToListAsync();
                author.Anime?.RemoveRange(0, author.Anime.Count);
                author.Anime?.AddRange(animeList);
            }

            if (author.MangaIdList != null && author.MangaIdList.Count > 0)
            {
                var mangaList = await _appDbContext.Manga.Where(a => author.MangaIdList.Any(m => m == a.Id)).ToListAsync();
                author.Manga?.RemoveRange(0, author.Manga.Count);
                author.Manga?.AddRange(mangaList);
            }

            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Author updated successfully!";

            return RedirectToAction("Index");

        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var author = await _appDbContext.Authors
                .Include(a => a.Positions)
                .Include(a => a.Manga)
                .Include(a => a.Anime)
                .Include(a => a.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (author == null)
                return NotFound();

            ViewBag.AnimeList = await _viewHelper.FillViewBagAnimeListAsync(_appDbContext);
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);

            if (author.Images != null && author.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(author.Images.Last().Data)));

            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var author = await _appDbContext.Authors
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (author == null)
                return NotFound();

            if (author.Images != null && author.Images.Count > 0)
            {
                _appDbContext.Images.RemoveRange(author.Images);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Authors.Remove(author);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Author deleted successfully!";

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> AddPositionToAuthor(Guid? animeId)
        //{

        //    var anime = await _appDbContext.Anime
        //        .Include(a => a.Authors)
        //        .FirstOrDefaultAsync(a => a.Id == animeId);

        //    ViewBag.PositionList = await _viewHelper.FillViewBagPositionListAsync(_appDbContext);

        //    if (anime != null && anime.Authors != null)
        //        return View(anime);
        //    else if(anime == null)
        //        return RedirectToAction("Index", "Anime");

        //    //if (manga != null && manga.Authors != null)
        //    //    return View(manga.Authors);
        //    //else if(manga == null)
        //    //    return RedirectToAction("Index", "Manga");

        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddPositionToAuthorPost(Guid? animeId)
        //{
        //    var anime = await _appDbContext.Anime
        //        .Include(a => a.Authors)
        //        .FirstOrDefaultAsync(a => a.Id == animeId);
        //    var positions = _appDbContext.Positions;

        //    if (anime != null && anime.Authors != null && anime.Authors.Count > 0)
        //        for (int i = 0; i < anime.Authors.Count; i++)
        //        {
        //            if (anime.Authors[i].PositionIdList != null && anime.Authors[i].PositionIdList?.Count > 0)
        //                anime.Authors[i].Positions?.AddRange(positions.Where(p => anime.Authors[i].PositionIdList.Any(x => x == p.Id)).ToList());

        //            for (int j = 0; j < anime.Authors[i].Positions.Count; j++)
        //                anime.Authors[i].Positions[j].Anime.Add(anime);

        //            _appDbContext.UpdateRange(positions);
        //            await _appDbContext.SaveChangesAsync();
        //        }
        //    //_appDbContext.Update(anime);
        //    //await _appDbContext.SaveChangesAsync();
        //    _appDbContext.UpdateRange(anime.Authors);
        //    await _appDbContext.SaveChangesAsync();

        //    _appDbContext.Update(anime);
        //    await _appDbContext.SaveChangesAsync();

            
        //    //TempData["success"] = "Author deleted successfully!";
        //    return RedirectToAction("Index");
        //}
    }
}
