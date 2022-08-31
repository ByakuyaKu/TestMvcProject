using JikanDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewModels;
using Anime = TestMvcProject.Models.Anime;

namespace TestMvcProject.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IViewHelper _viewHelper;


        public AnimeController(AppDbContext appDbContext, IViewHelper viewHelper)
        {
            _appDbContext = appDbContext;
            _viewHelper = viewHelper;
        }
        // GET: Anime
        public async Task<IActionResult> Index()
        {
            IEnumerable<Anime> AnimeList = await _appDbContext.Animies
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();
            return View(AnimeList);
        }

        // GET: Anime
        public async Task<IActionResult> IndexUneditable()
        {
            IEnumerable<Anime> AnimeList = await _appDbContext.Animies
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();
            return View(AnimeList);
        }

        // GET: Anime/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var anime = await _appDbContext.Animies
                .AsNoTracking()
                .Include(a => a.Manga)
                //.ThenInclude(m => m.Images)
                .Include(a => a.Images)
                .Include(a => a.Authors)
                //.ThenInclude(a => a.Images)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (anime == null)
                return NotFound();

            await _viewHelper.SearchAuthorsImagesAsync(anime.Authors, _appDbContext);

            if (anime.Images != null && anime.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(anime.Images.Last().Data)));

            return View(anime);
        }

        // GET: Anime/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);
            ViewBag.MangaList = await _viewHelper.FillViewBagMangaListAsync(_appDbContext);
            ViewBag.GenreList = await _viewHelper.FillViewBagGenreListAsync(_appDbContext);
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            var mangas = _appDbContext.Mangas;
            var authors = _appDbContext.Authors;
            var genres = _appDbContext.Genres;

            if (anime.Avatar != null)
            {
                var img = _viewHelper.GetImg(anime.Avatar, "AvatarOf" + anime.Tittle);
                anime.Images?.Add(img);
            }

            if (anime.MangaIdList != null && anime.MangaIdList.Count > 0)
                anime.Manga?.AddRange(mangas.Where(a => anime.MangaIdList.Any(m => m == a.Id)).ToList());

            if (anime.AuthorIdList != null && anime.AuthorIdList.Count > 0)
                anime.Authors?.AddRange(authors.Where(a => anime.AuthorIdList.Any(m => m == a.Id)).ToList());

            if (anime.GenreIdList != null && anime.GenreIdList.Count > 0)
                anime.Genres?.AddRange(genres.Where(g => anime.GenreIdList.Any(m => m == g.Id)).ToList());

            _appDbContext.Animies.Add(anime);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Anime created successfully!";

            return RedirectToAction("Index");
        }

        // GET: Anime/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _appDbContext.Animies
                .Include(m => m.Genres)
                .Include(m => m.Authors)
                .Include(m => m.Manga)
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (anime == null)
                return NotFound();

            ViewBag.MangaList = await _viewHelper.FillViewBagMangaListAsync(_appDbContext);
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);
            ViewBag.GenreList = await _viewHelper.FillViewBagGenreListAsync(_appDbContext);

            if (anime.Images != null && anime.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(anime.Images.Last().Data)));

            return View(anime);
        }

        // POST: Anime/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _appDbContext.Animies
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (anime == null)
                return NotFound();

            if (anime.Images != null && anime.Images.Count > 0)
            {
                _appDbContext.Images.RemoveRange(anime.Images);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Animies.Remove(anime);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Anime deleted successfully!";

            return RedirectToAction("Index");
        }

        // GET: Anime/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || Guid.Empty == id)
                return NotFound();

            var anime = await _appDbContext.Animies
                .Include(m => m.Images)
                .Include(m => m.Authors)
                .Include(m => m.Manga)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (anime == null)
                return NotFound();

            ViewBag.MangaList = await _viewHelper.FillViewBagMangaListAsync(_appDbContext);
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);
            ViewBag.GenreList = await _viewHelper.FillViewBagGenreListAsync(_appDbContext);

            return View(anime);
        }

        // POST: Anime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Anime anime)
        {
            if (anime == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(anime);

            var _anime = await _appDbContext.Animies
                .Include(m => m.Manga)
                .Include(m => m.Authors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == anime.Id);

            anime.Manga = _anime?.Manga;
            anime.Authors = _anime?.Authors;
            anime.Genres = _anime?.Genres;

            if (anime.Avatar != null)
            {
                var img = _viewHelper.GetImg(anime.Avatar, "PosterOf" + anime.Tittle);

                _appDbContext.Images.Add(img);
                await _appDbContext.SaveChangesAsync();

                anime.Images?.Add(img);
            }

            if (anime.MangaIdList != null && anime.MangaIdList.Count > 0)
            {
                var mangaList = await _appDbContext.Mangas.Where(a => anime.MangaIdList.Any(m => m == a.Id)).ToListAsync();
                anime.Manga?.RemoveRange(0, anime.Manga.Count);
                anime.Manga?.AddRange(mangaList);
            }

            if (anime.AuthorIdList != null && anime.AuthorIdList.Count > 0)
            {
                var authorList = await _appDbContext.Authors.Where(a => anime.AuthorIdList.Any(m => m == a.Id)).ToListAsync();
                anime.Authors?.RemoveRange(0, anime.Authors.Count);
                anime.Authors?.AddRange(authorList);
            }

            if (anime.GenreIdList != null && anime.GenreIdList.Count > 0)
            {
                var genreList = await _appDbContext.Genres.Where(g => anime.GenreIdList.Any(m => m == g.Id)).ToListAsync();
                anime.Genres?.RemoveRange(0, anime.Genres.Count);
                anime.Genres?.AddRange(genreList);
            }

            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Anime updated successfully!";

            return RedirectToAction("Index");

        }
    }
}
