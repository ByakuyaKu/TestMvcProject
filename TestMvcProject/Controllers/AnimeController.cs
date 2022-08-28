using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewModels;

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

            if (anime.Images != null && anime.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(anime.Images.Last().Data)));

            return View(anime);
        }

        //GET
        public async Task<IActionResult> Create()
        {
            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorList(_appDbContext);
            ViewBag.MangaList = await _viewHelper.FillViewBagMangaList(_appDbContext);
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            if (anime.Avatar != null)
            {
                var img = _viewHelper.GetImg(anime.Avatar, "PosterOf" + anime.Tittle);
                anime.Images.Add(img);
            }

            _appDbContext.Animies.Add(anime);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Anime created successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _appDbContext.Animies.FirstOrDefaultAsync(a => a.Id == id);

            if (anime == null)
                return NotFound();

            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorList(_appDbContext);
            ViewBag.MangaList = await _viewHelper.FillViewBagMangaList(_appDbContext);

            return View(anime);
        }

        // POST: PositionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _appDbContext.Animies.Include(a => a.Images).Include(a=>a.Manga).Include(a => a.Authors).ThenInclude(a => a.Positions).FirstOrDefaultAsync(a => a.Id == id);

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

        // GET: PositionController/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _appDbContext.Animies.FirstOrDefaultAsync(a => a.Id == id);

            if (anime == null)
                return NotFound();

            ViewBag.AuthorList = await _viewHelper.FillViewBagAuthorList(_appDbContext);
            ViewBag.MangaList = await _viewHelper.FillViewBagMangaList(_appDbContext);

            return View(anime);
        }

        // POST: PositionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            if (anime.Avatar != null)
            {
                var img = _viewHelper.GetImg(anime.Avatar, "PosterOf" + anime.Tittle);

                _appDbContext.Images.Add(img);
                await _appDbContext.SaveChangesAsync();

                anime.Images.Add(img);
            }

            _appDbContext.Animies.Update(anime);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Anime updated successfully!";

            return RedirectToAction("Index");
        }
    }
}
