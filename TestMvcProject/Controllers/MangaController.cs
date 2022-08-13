using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewModels;

namespace TestMvcProject.Controllers
{
    public class MangaController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public MangaController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: Manga
        public async Task<IActionResult> Index()
        {
            IEnumerable<Manga> MangaList = await _appDbContext.Mangas.Include(a => a.Animies).Include(a => a.Images).Include(a => a.Authors).ThenInclude(a => a.Positions).ToListAsync();
            return View(MangaList);
        }

        // GET: Manga/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var manga = await _appDbContext.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await FillViewBagAnimeList();
            ViewBag.AuthorList = await FillViewBagAuthorList();

            return View(manga);
        }

        // GET: Manga/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AnimeList = await FillViewBagAnimeList();
            ViewBag.AuthorList = await FillViewBagAuthorList();
            return View();
        }

        public async Task<SelectList> FillViewBagAuthorList()
        {
            var authors = await _appDbContext.Authors
                .Include(p => p.Positions)
                .Include(a => a.Images).ToListAsync();

            if (authors == null)
                authors = new List<Author>();

            return new SelectList(authors, "Id", "FirstName", "LastName");
        }

        public async Task<SelectList> FillViewBagAnimeList()
        {
            var animies = await _appDbContext.Animies
                .Include(a => a.Images)
                .Include(a => a.Manga).ToListAsync();

            if (animies == null)
                animies = new List<Anime>();

            return new SelectList(animies, "Id", "Tittle");
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

            if (manga.Avatar != null)
            {
                var img = ViewHelper.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);
                manga.Images.Add(img);
            }

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

            var manga = await _appDbContext.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await FillViewBagAnimeList();
            ViewBag.AuthorList = await FillViewBagAuthorList();

            return View(manga);
        }

        // POST: Manga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manga manga)
        {

            if (!ModelState.IsValid)
                return View(manga);

            if (manga.Avatar != null)
            {
                var img = ViewHelper.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);

                _appDbContext.Images.Add(img);
                await _appDbContext.SaveChangesAsync();

                manga.Images.Add(img);
            }

            _appDbContext.Mangas.Update(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga updated successfully!";

            return RedirectToAction("Index");

        }

        // GET: Manga/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _appDbContext.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await FillViewBagAnimeList();
            ViewBag.AuthorList = await FillViewBagAuthorList();

            return View(manga);
        }

        // POST: Manga/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _appDbContext.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            _appDbContext.Mangas.Remove(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga deleted successfully!";

            return RedirectToAction("Index");
        }

        //private bool MangaExists(Guid id)
        //{
        //    return (_appDbContext.Mangas?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
