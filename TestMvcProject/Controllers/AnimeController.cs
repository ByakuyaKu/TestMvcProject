using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AnimeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Anime> AnimeList = _appDbContext.Animies;
            return View(AnimeList);
        }


        //GET
        public ActionResult Details(Guid id)
        {
            IEnumerable<Anime> AnimeList = _appDbContext.Animies.Where(a=>a.Id == id).Include(a => a.Manga).Include(a => a.Authors).ToList();
            return View(AnimeList);
        }

        //GET
        public IActionResult Create()
        {
            //IEnumerable<Anime> AnimeList = _appDbContext.Animies.Include(a => a.Manga).Include(a => a.Authors).ToList();

            //IEnumerable<Anime> AnimeList = _appDbContext.Animies;
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            _appDbContext.Animies.Add(anime);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
