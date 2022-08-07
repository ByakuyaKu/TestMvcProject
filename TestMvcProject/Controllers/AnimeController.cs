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

        public IActionResult Create()
        {
            //IEnumerable<Anime> AnimeList = _appDbContext.Animies;
            return View();
        }
    }
}
