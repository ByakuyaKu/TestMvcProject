using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Xml.Linq;
using ImageMagick;
using JikanDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewModels;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Controllers
{
    public class MangaController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IViewHelper _viewHelper;

        public MangaController(AppDbContext appDbContext, IViewHelper viewHelper)
        {
            _appDbContext = appDbContext;
            _viewHelper = viewHelper;
        }

        // GET: Manga
        public async Task<IActionResult> Index()
        {
            IEnumerable<Manga> MangaList = await _appDbContext.Mangas
                .AsNoTracking()
                .Include(a => a.Images)
                .ToListAsync();
            return View(MangaList);
        }

        // GET: Manga/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var manga = await _appDbContext.Mangas
                .AsNoTracking()
                .Include(m => m.Animies)
                //.ThenInclude(a => a.Images)
                .Include(m => m.Images)
                .Include(m => m.Authors)
                //.ThenInclude(a => a.Images)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manga == null)
                return NotFound();

            await _viewHelper.SearchAuthorsImagesAsync(manga.Authors, _appDbContext);

            if (manga.Images != null && manga.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(manga.Images.Last().Data)));

            return View(manga);
        }

        // GET: Manga/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AnimeList = await _viewHelper.FillViewBagAnimeListAsync(_appDbContext);
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);
            ViewBag.GenreList = await _viewHelper.FillViewBagGenreListAsync(_appDbContext);
            return View();
        }

        // POST: Manga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manga manga)
        {
            if (!ModelState.IsValid)
                return View(manga);

            var animies = _appDbContext.Animies;
            var authors = _appDbContext.Authors;
            var genres = _appDbContext.Genres;

            if (manga.Avatar != null)
            {
                var img = _viewHelper.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);
                manga.Images?.Add(img);
            }

            if (manga.AnimeIdList != null && manga.AnimeIdList.Count > 0)
                manga.Animies?.AddRange(animies.Where(a => manga.AnimeIdList.Any(m => m == a.Id)).ToList());

            if (manga.AuthorIdList != null && manga.AuthorIdList.Count > 0)
                manga.Authors?.AddRange(authors.Where(a => manga.AuthorIdList.Any(m => m == a.Id)).ToList());

            if (manga.GenreIdList != null && manga.GenreIdList.Count > 0)
                manga.Genres?.AddRange(genres.Where(g => manga.GenreIdList.Any(m => m == g.Id)).ToList());

            _appDbContext.Mangas.Add(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga created successfully!";

            return RedirectToAction("Index");
        }

        // GET: Manga/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || Guid.Empty == id)
                return NotFound();

            var manga = await _appDbContext.Mangas
                .Include(m => m.Images)
                .Include(m => m.Authors)
                .Include(m => m.Animies)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await _viewHelper.FillViewBagAnimeListAsync(_appDbContext);
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);
            ViewBag.GenreList = await _viewHelper.FillViewBagGenreListAsync(_appDbContext);

            return View(manga);
        }

        // POST: Manga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manga manga)
        {
            if (manga == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(manga);

            var _manga = await _appDbContext.Mangas
                .Include(m => m.Animies)
                .Include(m => m.Authors)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == manga.Id);

            manga.Animies = _manga?.Animies;
            manga.Authors = _manga?.Authors;
            manga.Genres = _manga?.Genres;

            if (manga.Avatar != null)
            {
                var img = _viewHelper.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);

                _appDbContext.Images.Add(img);
                await _appDbContext.SaveChangesAsync();

                manga.Images?.Add(img);
            }

            if (manga.AnimeIdList != null && manga.AnimeIdList.Count > 0)
            {
                var animeList = await _appDbContext.Animies.Where(a => manga.AnimeIdList.Any(m => m == a.Id)).ToListAsync();
                manga.Animies?.RemoveRange(0, manga.Animies.Count);
                manga.Animies?.AddRange(animeList);
            }

            if (manga.AuthorIdList != null && manga.AuthorIdList.Count > 0)
            {
                var authorList = await _appDbContext.Authors.Where(a => manga.AuthorIdList.Any(m => m == a.Id)).ToListAsync();
                manga.Authors?.RemoveRange(0, manga.Authors.Count);
                manga.Authors?.AddRange(authorList);
            }

            if (manga.GenreIdList != null && manga.GenreIdList.Count > 0)
            {
                var genreList = await _appDbContext.Genres.Where(g => manga.GenreIdList.Any(m => m == g.Id)).ToListAsync();
                manga.Genres?.RemoveRange(0, manga.Genres.Count);
                manga.Genres?.AddRange(genreList);
            }

            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga updated successfully!";

            return RedirectToAction("Index");

        }

        // GET: Manga/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _appDbContext.Mangas
                .Include(m => m.Genres)
                .Include(m => m.Authors)
                .Include(m => m.Animies)
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await _viewHelper.FillViewBagAnimeListAsync(_appDbContext);
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorListAsync(_appDbContext);
            ViewBag.GenreList = await _viewHelper.FillViewBagGenreListAsync(_appDbContext);

            if (manga.Images != null && manga.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(manga.Images.Last().Data)));

            return View(manga);
        }

        // POST: Manga/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _appDbContext.Mangas
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            if (manga.Images != null && manga.Images.Count > 0)
            {
                _appDbContext.Images.RemoveRange(manga.Images);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Mangas.Remove(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga deleted successfully!";

            return RedirectToAction("Index");
        }

        // GET: Manga
        public async Task<IActionResult> IndexUneditable()
        {
            IEnumerable<Manga> MangaList = await _appDbContext.Mangas
                .AsNoTracking()
                .Include(a => a.Images)

                .ToListAsync();
            return View(MangaList);
        }
    }
}
