using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewHelperLib;

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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> AuthorList = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();
            return View(AuthorList);
        }

        // GET: Author
        public async Task<IActionResult> IndexUneditable()
        {
            IEnumerable<Author> AuthorList = await _appDbContext.Authors
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();
            return View(AuthorList);
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
    }
}
