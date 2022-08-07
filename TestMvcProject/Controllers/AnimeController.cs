using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
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
